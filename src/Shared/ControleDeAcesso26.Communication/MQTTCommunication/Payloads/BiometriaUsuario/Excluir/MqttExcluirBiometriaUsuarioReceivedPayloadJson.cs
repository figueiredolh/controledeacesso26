namespace ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Excluir
{
    public class MqttExcluirBiometriaUsuarioReceivedPayloadJson
    {
        public bool Excluido { get; init; }
        public int? Codigo { get; init; }
    }
}
