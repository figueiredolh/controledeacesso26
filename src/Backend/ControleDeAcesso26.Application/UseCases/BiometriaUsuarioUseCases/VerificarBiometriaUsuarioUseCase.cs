using ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases.Interfaces;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Verificar;
using ControleDeAcesso26.Communication.MQTTCommunication.Payloads.Porta;
using ControleDeAcesso26.Communication.MQTTCommunication.Topics;
using ControleDeAcesso26.Domain.Entities;
using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using ControleDeAcesso26.Domain.Interfaces.ITemplateBiometriaUsuario;
using ControleDeAcesso26.Exceptions.Exceptions;
using ControleDeAcesso26.Exceptions.ValidatorsRulesResourceMessages;
using Microsoft.Extensions.DependencyInjection;

namespace ControleDeAcesso26.Application.UseCases.BiometriaUsuarioUseCases
{
    public class VerificarBiometriaUsuarioUseCase : IVerificarBiometriaUsuarioUseCase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMqttPublisher<MqttAbrirPortaPublishPayloadJson> _mqttPublisher;
        public VerificarBiometriaUsuarioUseCase(IServiceScopeFactory serviceScopeFactory,
                                                IMqttPublisher<MqttAbrirPortaPublishPayloadJson> mqttPublisher)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mqttPublisher = mqttPublisher;
        }
        public async Task Execute(MqttVerificarBiometriaUsuarioReceivedPayloadJson payload, string topic)
        {
            TemplateBiometriaUsuario? templateBiometriaUsuario = null!;
            
            string acao = payload.Sensor.Equals(1) ? "S" : "E"; //S - Saída; E - Entrada
            DateTime horarioAtual = DateTime.Now;
            
            //verificar se usuário com o respectivo id do sensor está ativo
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var templateBiometriaReadOnlyRepository = scope.ServiceProvider.GetRequiredService<ITemplateBiometriaReadOnlyRepository>();

                switch (topic)
                {
                    case MqttTopics.VerificarBiometriaUsuarioSensor1:
                        var usuarioAtivoS1 = await templateBiometriaReadOnlyRepository.UsuarioAtivo(payload.IdSensor);

                        if (usuarioAtivoS1 == false)
                        {
                            throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO);
                        }

                        templateBiometriaUsuario = await templateBiometriaReadOnlyRepository.BuscarTemplatePorId(payload.IdSensor);
                        break;

                    case MqttTopics.VerificarBiometriaUsuarioSensor2:
                        var usuarioAtivoS2 = await templateBiometriaReadOnlyRepository.UsuarioAtivo(payload.IdSensor, 2);

                        if (usuarioAtivoS2 == false)
                        {
                            throw new NotFoundException(ValidatorsRulesResourceMessages.USUARIO_NAO_ENCONTRADO);
                        }

                        templateBiometriaUsuario = await templateBiometriaReadOnlyRepository.BuscarTemplatePorId(payload.IdSensor);
                        break;
                }
            }

            string nomeUsuario = templateBiometriaUsuario!.Usuario.Nome;
                
            //registrar entrada ou saída nos registros
                
            //abrir porta, enviando nome de usuário, ação (entrada ou saída) e horário

            var payloadAbrirPorta = new MqttAbrirPortaPublishPayloadJson()
            {
                NomeUsuario = nomeUsuario,
                Acao = acao,
                Horario = horarioAtual
            };

            await _mqttPublisher.PublishMessage(MqttTopics.AbrirPorta, payloadAbrirPorta);

            await Task.CompletedTask;
        }
    }
}
