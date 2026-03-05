using ControleDeAcesso26.Communication.MQTTCommunication.Topics;
using ControleDeAcesso26.Infrastructure.Messaging.MQTT;
using Microsoft.Extensions.Options;
using MQTTnet;

namespace ControleDeAcesso26.API.MQTT
{
    public class MqttInitializer : BackgroundService
    {
        private readonly MqttClientFactory _mqttClientFactory;
        private readonly MqttClientOptions _options;
        private readonly IMqttClient _mqttClient;
        private readonly IServiceScopeFactory _serviceScope;
        public MqttInitializer(MqttClientFactory mqttClientFactory, IOptions<MqttSettings> settings,
                            IMqttClient mqttClient, IServiceScopeFactory serviceScope)
        {
            _mqttClientFactory = mqttClientFactory;
            _options = new MqttClientOptionsBuilder()
                                       .WithTcpServer(settings.Value.Server, settings.Value.Port)
                                       .WithCredentials(settings.Value.Hostname, settings.Value.Password)
                                       .WithTlsOptions(opt =>
                                       {
                                           opt.UseTls();
                                       })
                                       .Build(); ;
            _mqttClient = mqttClient;
            _serviceScope = serviceScope;
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
                    _mqttClient.ApplicationMessageReceivedAsync += async e =>
                    {
                        using var scope = _serviceScope.CreateScope();
                        var topic = e.ApplicationMessage.Topic;
                        //var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                        /*switch (topic)
                        {
                            case MqttTopics.LeituraRfidUsuario:
                                var payloadDeserialized = JsonSerializer.Deserialize<MqttLeituraUsuarioRfidPayload>(payload)!;
                                var useCase = scope.ServiceProvider.GetRequiredService<IMqttLeituraRfidUsuarioUseCase>();

                                await useCase.Execute(payloadDeserialized);
                                break;
                            default:
                                break;
                        };*/
                    };

                    if (!await _mqttClient.TryPingAsync()) //verifica se um pequeno pacote de controle (PINGREQ) não chega ao broker
                        await _mqttClient.ConnectAsync(_options, stoppingToken);

                    break; // Conectou! Sai do loop de tentativa inicial
                }
                catch
                {
                    await Task.Delay(5000, stoppingToken); // Espera 5s para tentar de novo
                }
            }

            var mqttSubscribeOptions = _mqttClientFactory.CreateSubscribeOptionsBuilder()
                                   .WithTopicFilter(MqttTopics.Teste, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   .WithTopicFilter(MqttTopics.CadastrarBiometriaUsuarioEnviarDados)
                                   .WithTopicFilter(MqttTopics.ExcluirBiometriaUsuarioEnviarDadosSensor1)
                                   //.WithTopicFilter(MqttTopics.ExcluirBiometriaUsuarioEnviarDadosSensor2)
                                   //.WithTopicFilter(MqttTopics.CadastrarBiometriaUsuarioEnviarDados, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   //.WithTopicFilter(MqttTopics.CadastrarRfidUsuario, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   //.WithTopicFilter(MqttTopics.LeituraRfidUsuario, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   .Build();

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            Console.WriteLine("Cliente Mqtt Inscrito");
        }
    }
}
