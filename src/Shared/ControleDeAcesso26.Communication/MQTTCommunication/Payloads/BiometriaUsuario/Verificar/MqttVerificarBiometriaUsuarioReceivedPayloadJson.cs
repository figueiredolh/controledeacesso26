namespace ControleDeAcesso26.Communication.MQTTCommunication.Payloads.BiometriaUsuario.Verificar
{
    public class MqttVerificarBiometriaUsuarioReceivedPayloadJson
    {
        public required int IdSensor { get; set; }
        public required int Sensor { get; set; }
    }
}
