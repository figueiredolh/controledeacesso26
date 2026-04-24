using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Excluir;
using ControleDeAcesso26.Communication.MQTTCommunication.Topics;
using ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases
{
    public class ExcluirBiometriaUsuarioUseCase : IExcluirBiometriaUsuarioUseCase
    {
        private TemplateBiometriaUsuario? templateBiometriaUsuario;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMqttPublisher<MqttExcluirBiometriaUsuarioPublishPayloadJson> _publisher;
        private readonly IMqttHandler<MqttExcluirBiometriaUsuarioReceivedPayloadJson> _handler;

        public ExcluirBiometriaUsuarioUseCase(IServiceScopeFactory serviceScopeFactory,
                                              IMqttPublisher<MqttExcluirBiometriaUsuarioPublishPayloadJson> publisher,
                                              IMqttHandler<MqttExcluirBiometriaUsuarioReceivedPayloadJson> handler)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _publisher = publisher;
            _handler = handler;
        }
        public async Task<ResponseExcluirBiometriaUsuarioJson> Execute(int idSensor)
        {
            //verificar se um template foi cadastrado no id do sensor informado
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var templateBiometriaUsuarioReadOnlyRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaReadOnlyRepository>();
                templateBiometriaUsuario = await templateBiometriaUsuarioReadOnlyRepository.BuscarTemplatePorId(idSensor);
            }

            if (templateBiometriaUsuario is null)
            {
                throw new NotFoundException(ValidatorsRulesResourceMessages.TEMPLATE_NAO_ENCONTRADO_BANCO);
            }

            var payload = new MqttExcluirBiometriaUsuarioPublishPayloadJson()
            {
                IdSensor = idSensor,
            };

            try
            {
                await _publisher.PublishMessage(MqttTopics.ExcluirBiometriaUsuarioSensor1, payload);
                var dadosEnviadosSensor1 = await _handler.HandleMessage(MqttTopics.ExcluirBiometriaUsuarioEnviarDadosSensor1);

                //excluir template no slot relacionado no sensor 2:
                /*await _publisher.PublishMessage(MqttTopics.ExcluirBiometriaUsuarioSensor2, payload);
                var statusExcluidoSensor2 = await _handler.HandleMessage(MqttTopics.ExcluirBiometriaUsuarioSensor2);*/

                if (dadosEnviadosSensor1.CodigoErro == 1)
                {
                    throw new SensorAlreadyOccupiedException(ValidatorsRulesResourceMessages.SENSOR1_ERRO_EXCLUIR_TEMPLATE);
                }

                if (dadosEnviadosSensor1.CodigoErro == 2)
                {
                    throw new SensorSaveTemplateException(dadosEnviadosSensor1.IdSensor, ValidatorsRulesResourceMessages.SENSOR1_ERRO_EXCLUIR_TEMPLATE);
                }

                if (dadosEnviadosSensor1.CodigoErro == 0) //garantia de que o template tenha sido deletado com sucesso
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var templateBiometriaDeleteRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaDeleteRepository>();

                        await templateBiometriaDeleteRepository.ExcluirTemplate(idSensor);
                    }
                }

                var response = new ResponseExcluirBiometriaUsuarioJson()
                {
                    NomeUsuario = templateBiometriaUsuario.Usuario.Nome,
                    IdSensor1 = templateBiometriaUsuario.IdSensor1,
                };

                return response;
            }
            catch (TimeoutException)
            {
                await _publisher.PublishMessage(MqttTopics.ExcluirBiometriaUsuarioSensor1,
                                                new MqttExcluirBiometriaUsuarioPublishPayloadJson() { IdSensor = 0 });
                throw new TimeoutException(ValidatorsRulesResourceMessages.SENSOR_TIMEOUT);
            }
        }
    }
}
