namespace ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Cadastro
{
    public class MqttCadastrarBiometriaUsuarioReceivedPayloadJson
    {
        public string? UsuarioTemplate { get; set; }
        public int IdSensor { get; set; }
        public int? CodigoErro { get; set; }
    }
}
