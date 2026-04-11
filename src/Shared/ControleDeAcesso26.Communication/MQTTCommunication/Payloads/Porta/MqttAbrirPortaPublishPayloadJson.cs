namespace ControleDeAcesso26.Communication.MQTTCommunication.Payloads.Porta
{
    public class MqttAbrirPortaPublishPayloadJson
    {
        public required string NomeUsuario { get; init; }
        public required string Acao { get; init; }
        public required DateTime Horario { get; init; }
    }
}
