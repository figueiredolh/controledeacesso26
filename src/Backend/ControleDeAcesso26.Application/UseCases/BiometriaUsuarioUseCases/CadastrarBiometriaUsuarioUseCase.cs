using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Cadastro;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Cadastro.CadastroEtapas;
using ControleDeAcesso26.Communication.MQTTCommunication.Topics;
using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Domain.Interfaces.IUnitOfWork;
using ControleDeAcesso26.Domain.Interfaces.IUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases
{
    public class CadastrarBiometriaUsuarioUseCase : ICadastrarBiometriaUsuarioUseCase
    {
        private Usuario? usuario;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMqttPublisher<MqttCadastrarBiometriaUsuarioPublishPayloadJson> _publisher;
        private readonly IMqttHandler<MqttCadastrarBiometriaUsuarioReceivedPayloadJson> _handler;
        public CadastrarBiometriaUsuarioUseCase(IMqttPublisher<MqttCadastrarBiometriaUsuarioPublishPayloadJson> publisher,
                                                IMqttHandler<MqttCadastrarBiometriaUsuarioReceivedPayloadJson> handler,
                                                IServiceScopeFactory serviceScopeFactory)
        {
            _publisher = publisher;
            _handler = handler;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<ResponseCadastrarBiometriaUsuarioJson> Execute(long idUsuario)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var usuarioReadOnlyRepository = scope.ServiceProvider.GetRequiredService<IUsuarioReadOnlyRepository>();
                usuario = await usuarioReadOnlyRepository.RecuperarUsuarioPorId(idUsuario, true, true);
            }

            if (usuario is null)
                throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO);

            var payload = new MqttCadastrarBiometriaUsuarioPublishPayloadJson()
            {
                ColetarDados = true,
                CadastroEtapa = CadastroEtapas.IniciarCadastro
            };

            try
            {
                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuarioSensor1, payload);
                var dadosBiometriaUsuarioSensor1 = await _handler.HandleMessage(MqttTopics.CadastrarBiometriaUsuarioEnviarDadosSensor1);

                if (dadosBiometriaUsuarioSensor1.CodigoErro == 1)
                    throw new SensorAlreadyOccupiedException(ValidatorsRulesResourceMessages.SENSOR1_OCUPADO);

                if (dadosBiometriaUsuarioSensor1.CodigoErro == 2)
                    throw new MemorySensorSlotAlreadyOccupiedException(dadosBiometriaUsuarioSensor1.IdSensor, ValidatorsRulesResourceMessages.SENSOR1_SLOTS_OCUPADOS);

                if (dadosBiometriaUsuarioSensor1.CodigoErro == 3)
                    throw new SensorOperationCanceledException(ValidatorsRulesResourceMessages.SENSOR1_OPERACAO_CANCELADA);

                if (dadosBiometriaUsuarioSensor1.CodigoErro == 4)
                    throw new AttemptLimitReachedException(ValidatorsRulesResourceMessages.SENSOR1_LIMITE_TENTATIVA);

                //caso passe pelo handler anterior: envia confirmação e aprovação da gravação do template no sensor
                var payloadConfirmacao = new MqttCadastrarBiometriaUsuarioPublishPayloadJson()
                {
                    ColetarDados = true,
                    CadastroEtapa = CadastroEtapas.SucessoRecebimentoEAprovarGravacao
                };

                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuarioSensor1, payloadConfirmacao);
                var sucessoGravacaoSensor1 = await _handler.HandleMessage(MqttTopics.CadastrarBiometriaUsuarioEnviarDadosSensor1);

                if (sucessoGravacaoSensor1.CodigoErro == 5)
                {
                    throw new SensorSaveTemplateException(sucessoGravacaoSensor1.IdSensor, ValidatorsRulesResourceMessages.SENSOR1_ERRO_SALVAR_TEMPLATE);
                }

                if (sucessoGravacaoSensor1.CodigoErro == 0) //salvar no banco - garantia após o sucesso da gravação do template no sensor
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var templateReadOnlyRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaReadOnlyRepository>();
                        var templateWriteRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaWriteRepository>();
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var idSensor1JaExiste = await templateReadOnlyRepository.IdSensor1JaExiste((int)dadosBiometriaUsuarioSensor1.IdSensor!);
                        if (idSensor1JaExiste)
                        {
                            throw new MemorySensorSlotAlreadyOccupiedException((int)dadosBiometriaUsuarioSensor1.IdSensor, ValidatorsRulesResourceMessages.SENSOR1_POSICAO_MEMORIA_UTILIZADA);
                        }

                        TemplateBiometriaUsuario templateBD = new()
                        {
                            IdSensor1 = (int)dadosBiometriaUsuarioSensor1.IdSensor,
                            IdUsuario = usuario.Id,
                            Template = Convert.FromHexString(dadosBiometriaUsuarioSensor1.UsuarioTemplate!)
                        };

                        await templateWriteRepository.ArmazenarTemplate(templateBD);
                        await unitOfWork.SalvarMudancas();
                    }
                }                

                //enviar response para front-end: nome do usuário e status (com id do sensor)
                var response = new ResponseCadastrarBiometriaUsuarioJson()
                {
                    NomeUsuario = usuario.Nome,
                    UsuarioTemplate = dadosBiometriaUsuarioSensor1.UsuarioTemplate,
                    IdSensor = dadosBiometriaUsuarioSensor1.IdSensor,
                    Status = "Dados biométricos salvos com sucesso!"
                };

                return response;
            }
            catch (TimeoutException)
            {
                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuarioSensor1,
                                                new MqttCadastrarBiometriaUsuarioPublishPayloadJson() { ColetarDados = false });
                throw new TimeoutException(ValidatorsRulesResourceMessages.SENSOR_TIMEOUT);
            }
        }
    }
}
