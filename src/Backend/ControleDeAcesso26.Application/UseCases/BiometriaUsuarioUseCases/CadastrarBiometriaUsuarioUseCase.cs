using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Cadastro;
using ControleDeAcesso26.Communication.MQTTCommunication.Topics;
using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IMqtt;
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
                ColetarDados = "true"
            };

            try
            {
                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuario, payload);
                var dadosBiometriaUsuario = await _handler.HandleMessage(MqttTopics.CadastrarBiometriaUsuarioEnviarDados);

                //salvar no banco - tabela não criada

                //enviar response para front-end: nome do usuário e status (com id do sensor)
                var response = new ResponseCadastrarBiometriaUsuarioJson()
                {
                    NomeUsuario = usuario.Nome,
                    IdSensor = dadosBiometriaUsuario.IdSensor,
                    Status = "Dados biométricos salvos com sucesso!"
                };

                return response;
            }
            catch (TimeoutException exception)
            {
                await _publisher.PublishMessage(MqttTopics.CadastrarBiometriaUsuario,
                                                new MqttCadastrarBiometriaUsuarioPublishPayloadJson() { ColetarDados = "false" });
                throw new TimeoutException(exception.Message);
            }
            finally
            {
                await Task.CompletedTask;
            }
        }
    }
}
