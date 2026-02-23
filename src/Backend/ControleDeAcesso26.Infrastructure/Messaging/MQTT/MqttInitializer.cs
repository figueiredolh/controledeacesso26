using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using System.Text;

namespace ControleDeAcesso26.Infrastructure.Messaging.MQTT
{
    public class MqttInitializer : BackgroundService
    {
        private readonly MqttClientFactory _mqttClientFactory;
        private readonly IMqttClient _mqttClient;
        private readonly MqttClientOptions _options;

        public MqttInitializer(MqttClientFactory mqttClientFactory, IMqttClient mqttClient, IOptions<MqttSettings> settings)
        {
            _mqttClientFactory = mqttClientFactory;
            _mqttClient = mqttClient;
            _options = new MqttClientOptionsBuilder()
                                       .WithTcpServer(settings.Value.Server, settings.Value.Port)
                                       .WithCredentials(settings.Value.Hostname, settings.Value.Password)
                                       .WithTlsOptions(opt =>
                                       {
                                           opt.UseTls();
                                       })
                                       .Build();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _mqttClient.DisconnectedAsync += async e =>
            {
                Console.WriteLine("Conexão caiu. Tentando reconectar em 5s...");
                await Task.Delay(TimeSpan.FromSeconds(5));

                // Tenta reconectar. Se falhar aqui, o evento disparará novamente (recursão segura)
                await _mqttClient.ConnectAsync(_options, CancellationToken.None);
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //teste de mensagem enviada do broker - remover ou melhorar posteriormente

                    /*_mqttClient.ApplicationMessageReceivedAsync += e =>
                    {
                        Console.WriteLine("Received application message.");

                        var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        Console.WriteLine($"Payload: {payload}");

                        return Task.CompletedTask;
                    };*/

                    if (!await _mqttClient.TryPingAsync()) //verifica se um pequeno pacote de controle (PINGREQ) não chega ao broker
                        await _mqttClient.ConnectAsync(_options, stoppingToken);

                    var mqttSubscribeOptions = _mqttClientFactory.CreateSubscribeOptionsBuilder()
                                               .WithTopicFilter("ControleDeAcesso26/teste", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                               .Build();

                    await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                    Console.WriteLine("Cliente Mqtt Inscrito");

                    break; // Conectou! Sai do loop de tentativa inicial
                }
                catch
                {
                    await Task.Delay(5000, stoppingToken); // Espera 5s para tentar de novo
                }
            }
        }
    }
}