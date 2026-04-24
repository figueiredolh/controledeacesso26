namespace ControleDeAcesso26.Infrastructure.Messaging.MQTT
{
    public class MqttSettings
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Hostname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
