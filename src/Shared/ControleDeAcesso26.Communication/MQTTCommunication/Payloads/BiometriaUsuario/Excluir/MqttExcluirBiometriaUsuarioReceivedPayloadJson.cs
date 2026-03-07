namespace ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Excluir
{
    public class MqttExcluirBiometriaUsuarioReceivedPayloadJson
    {
        public required int CodigoErro { get; set; }
        public int IdSensor { get; set; }
    }
}
