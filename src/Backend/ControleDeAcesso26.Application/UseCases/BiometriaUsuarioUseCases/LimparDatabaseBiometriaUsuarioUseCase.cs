using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Excluir;
using ControleDeAcesso26.Communication.MQTTCommunication.Topics;
using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases
{
    public class LimparDatabaseBiometriaUsuarioUseCase : ILimparDatabaseBiometriaUsuarioUseCase
    {
        private int _registrosTemplatesBiometria;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMqttPublisher<MqttExcluirBiometriaUsuarioPublishPayloadJson> _publisher;
        private readonly IMqttHandler<MqttExcluirBiometriaUsuarioReceivedPayloadJson> _handler;

        public LimparDatabaseBiometriaUsuarioUseCase(IServiceScopeFactory serviceScopeFactory,
                                              IMqttPublisher<MqttExcluirBiometriaUsuarioPublishPayloadJson> publisher,
                                              IMqttHandler<MqttExcluirBiometriaUsuarioReceivedPayloadJson> handler)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _publisher = publisher;
            _handler = handler;
        }
        public async Task<ResponseLimparDatabaseBiometriaUsuarioJson> Execute(string palavraConfirmacao)
        {
            if (palavraConfirmacao != "deletar")
            {
                throw new Exception("Texto incorreto"); //criar exceção específica
            }

            //verificar se um template foi cadastrado no id do sensor informado
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var templateBiometriaUsuarioReadOnlyRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaReadOnlyRepository>();
                _registrosTemplatesBiometria = await templateBiometriaUsuarioReadOnlyRepository.TotalRegistrosTemplates();

                if (_registrosTemplatesBiometria > 0)
                {
                    var templateBiometriaUsuarioDeleteRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaDeleteRepository>();
                    await templateBiometriaUsuarioDeleteRepository.LimparDatabase();
                }
            }

            try
            {
                //publicar comando para excluir
                //receber confirmação da exclusão?
                //enviar resposta ao usuário - "comando enviado" ou "comando enviado com sucesso"
                var payload = new MqttExcluirBiometriaUsuarioPublishPayloadJson()
                {
                    IdSensor = 1,
                }; //temporário?

                await _publisher.PublishMessage(MqttTopics.LimparDatabaseBiometriaSensor1, payload);

                return new ResponseLimparDatabaseBiometriaUsuarioJson();
            }
            catch (TimeoutException)
            {
                throw new TimeoutException(ValidatorsRulesResourceMessages.SENSOR_TIMEOUT);
            }
        }
    }
}
