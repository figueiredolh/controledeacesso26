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

            // Assina o evento temporariamente
            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;

            // 3. Aguarda o MQTT com um Timeout
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
            var firstCompletedTask = Task.WhenAny(taskCompletedSource.Task, timeoutTask);

            if (timeoutTask == firstCompletedTask)
            {
                throw new TimeoutException("O sensor não enviou os dados necessários no tempo.");
            }

            var mqttData = await taskCompletedSource.Task;
            _mqttClient.ApplicationMessageReceivedAsync -= OnMessageReceived;

            return mqttData;
        }
    }
}
