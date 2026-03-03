using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using MQTTnet;
using System.Text.Json;

namespace ControleDeAcesso26.Infrastructure.Messaging.MQTT
{
    public class MqttMessagePublisher<T> : IMqttPublisher<T>
    {
        private readonly IMqttClient _mqttClient;
        public MqttMessagePublisher(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }
        public async Task PublishMessage(string topic, T payload)
        {
            var payloadString = JsonSerializer.Serialize<T>(payload);

            var applicationMessage = new MqttApplicationMessageBuilder()
                                    .WithTopic(topic)
                                    .WithPayload(payloadString)
                                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                    .Build();

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        }
    }
}
