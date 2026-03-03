using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Cadastro;
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
                ColetarDados = true
            };

            try
            {
                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuario, payload);
                var dadosBiometriaUsuario = await _handler.HandleMessage(MqttTopics.CadastrarBiometriaUsuarioEnviarDados);

                //salvar no banco - tabela não criada
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var templateReadOnlyRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaReadOnlyRepository>();
                    var templateWriteRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaWriteRepository>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var idSensor1JaExiste = await templateReadOnlyRepository.IdSensor1JaExiste(dadosBiometriaUsuario.IdSensor);
                    if (idSensor1JaExiste)
                    {
                        throw new MemorySensorSlotAlreadyOccupiedException(ValidatorsRulesResourceMessages.POSICAO_MEMORIA_SENSOR1_UTILIZADA);
                    }

                    TemplateBiometriaUsuario templateBD = new()
                    {
                        IdSensor1 = dadosBiometriaUsuario.IdSensor,
                        IdUsuario = usuario.Id,
                        Template = Convert.FromHexString(dadosBiometriaUsuario.UsuarioTemplate!)
                    };

                    await templateWriteRepository.ArmazenarTemplate(templateBD);
                    await unitOfWork.SalvarMudancas();
                }

                //enviar response para front-end: nome do usuário e status (com id do sensor)
                var response = new ResponseCadastrarBiometriaUsuarioJson()
                {
                    NomeUsuario = usuario.Nome,
                    UsuarioTemplate = dadosBiometriaUsuario.UsuarioTemplate,
                    IdSensor = dadosBiometriaUsuario.IdSensor,
                    Status = "Dados biométricos salvos com sucesso!"
                };

                return response;
            }
            catch (TimeoutException)
            {
                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuario,
                                                new MqttCadastrarBiometriaUsuarioPublishPayloadJson() { ColetarDados = false });
                throw new TimeoutException(ValidatorsRulesResourceMessages.SENSOR_TIMEOUT);
            }
        }
    }
}
