using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using MQTTnet;
using System.Text;
using System.Text.Json;

namespace ControleDeAcesso26.Infrastructure.Messaging.MQTT
{
    public class MqttMessageHandler<T> : IMqttHandler<T>
    {
        private readonly IMqttClient _mqttClient;
        public MqttMessageHandler(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient;
        }
        public async Task<T> HandleMessage(string topic)
        {            
            // 1. Criamos uma promessa que será resolvida quando o MQTT chegar
            var taskCompletedSource = new TaskCompletionSource<T>();

            // 2. Definimos o que fazer ao receber a mensagem
            Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
            {
                if (e.ApplicationMessage.Topic == topic)
                {
                    var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    var mqttData = JsonSerializer.Deserialize<T>(payload);

                    taskCompletedSource.TrySetResult(mqttData!); //envia ao mqttData - linha 45
                }

                return taskCompletedSource.Task;
            }           

            try
            {
                // Assina o evento temporariamente
                _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;

                var mqttData = await taskCompletedSource.Task.WaitAsync(TimeSpan.FromSeconds(30));

                return mqttData;
            }
            catch(TimeoutException)
            {
                throw new TimeoutException();
            }
            finally
            {
                _mqttClient.ApplicationMessageReceivedAsync -= OnMessageReceived;
            }            
        }
    }
}
