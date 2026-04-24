namespace ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Cadastro
{
    public class MqttCadastrarBiometriaUsuarioPublishPayloadJson
    {
        public required bool ColetarDados { get; set; }
        public int CadastroEtapa { get; set; }
    }
}
