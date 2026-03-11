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
                                       .WithClientId("dotnet-backend-service")
                                       .WithTlsOptions(opt =>
                                       {
                                           opt.UseTls();
                                           opt.WithCertificateValidationHandler(_ => true);
                                       })
                                       .WithCleanSession(false)
                                       .WithCleanStart(false)
                                       .Build();
            _mqttClient = mqttClient;
            _serviceScope = serviceScope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var mqttSubscribeOptions = _mqttClientFactory.CreateSubscribeOptionsBuilder()
                                   .WithTopicFilter(MqttTopics.Teste, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   .WithTopicFilter(MqttTopics.CadastrarBiometriaUsuarioEnviarDadosSensor1, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   .WithTopicFilter(MqttTopics.CadastrarBiometriaUsuarioEnviarDadosSensor1Feedback)
                                   .WithTopicFilter(MqttTopics.ExcluirBiometriaUsuarioEnviarDadosSensor1, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   //.WithTopicFilter(MqttTopics.ExcluirBiometriaUsuarioEnviarDadosSensor2)
                                   //.WithTopicFilter(MqttTopics.CadastrarBiometriaUsuarioEnviarDados, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   //.WithTopicFilter(MqttTopics.CadastrarRfidUsuario, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   //.WithTopicFilter(MqttTopics.LeituraRfidUsuario, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                                   .Build();

            _mqttClient.DisconnectedAsync += async e =>
            {
                try
                {
                    Console.WriteLine("Conexão caiu. Tentando reconectar em 5s...");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                    // Verifica se o app ainda está rodando antes de tentar
                    if (!stoppingToken.IsCancellationRequested)
                    {
                        await _mqttClient.ConnectAsync(_options, stoppingToken);

                        // Re-assina os tópicos (importante se o broker perder a sessão)
                        await _mqttClient.SubscribeAsync(mqttSubscribeOptions, stoppingToken);

                        Console.WriteLine("Reconectado e tópicos assinados com sucesso.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro ao tentar reconectar. Tentando novamente...");
                }                
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

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, stoppingToken);

            Console.WriteLine("Cliente Mqtt Inscrito");
        }
    }
}
