using ControleDeAcesso26.Domain.Interfaces.IMqtt;
using MQTTnet;
using MQTTnet.Internal;
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
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var taskCompletedSource = new TaskCompletionSource<T>();

            // 2. Definimos o que fazer ao receber a mensagem
            Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
            {    
                if (e.ApplicationMessage.Topic == topic)
                {
                    // Renovação do Timeout
                    cts.CancelAfter(TimeSpan.FromSeconds(5));

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

                var mqttData = await taskCompletedSource.Task.WaitAsync(cts.Token);

                return mqttData;
            }
            catch(OperationCanceledException)
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
