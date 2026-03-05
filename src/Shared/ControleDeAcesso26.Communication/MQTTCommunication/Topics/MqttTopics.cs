namespace ControleDeAcesso26.Communication.MQTTCommunication.Topics
{
    public static class MqttTopics
    {
        public const string Teste = "controledeacesso26/teste";
        public const string CadastrarBiometriaUsuario = "controledeacesso26/biometria/cadastro/controle";
        public const string CadastrarBiometriaUsuarioEnviarDados = "controledeacesso26/biometria/cadastro/controle/enviar";

        public const string ExcluirBiometriaUsuarioSensor1 = "controledeacesso26/biometria/excluir/sensor1";
        public const string ExcluirBiometriaUsuarioSensor2 = "controledeacesso26/biometria/excluir/sensor2";
        public const string ExcluirBiometriaUsuarioEnviarDadosSensor1 = "controledeacesso26/biometria/excluir/sensor1/enviar";
        public const string ExcluirBiometriaUsuarioEnviarDadosSensor2 = "controledeacesso26/biometria/excluir/sensor2/enviar";
        //public const string CadastrarRfidUsuario = "controledeacesso26/cadastro/rfid/cadastrardados";
        //public const string LeituraRfidUsuario = "controledeacesso26/rfid/lerdados";
    }
}
