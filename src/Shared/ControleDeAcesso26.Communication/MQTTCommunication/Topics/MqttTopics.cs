namespace ControleDeAcesso26.Communication.MQTTCommunication.Topics
{
    public static class MqttTopics
    {
        public const string CadastrarBiometriaUsuarioSensor1 = "controledeacesso26/biometria/cadastro/sensor1";
        public const string CadastrarBiometriaUsuarioEnviarDadosSensor1 = "controledeacesso26/biometria/cadastro/sensor1/enviar";
        public const string CadastrarBiometriaUsuarioEnviarDadosSensor1Feedback = "controledeacesso26/biometria/cadastro/sensor1/enviar/feedback";

        public const string ExcluirBiometriaUsuarioSensor1 = "controledeacesso26/biometria/excluir/sensor1";
        public const string ExcluirBiometriaUsuarioSensor2 = "controledeacesso26/biometria/excluir/sensor2";
        public const string ExcluirBiometriaUsuarioEnviarDadosSensor1 = "controledeacesso26/biometria/excluir/sensor1/enviar";
        public const string ExcluirBiometriaUsuarioEnviarDadosSensor2 = "controledeacesso26/biometria/excluir/sensor2/enviar";
        public const string LimparDatabaseBiometriaSensor1 = "controledeacesso26/biometria/excluirdb/sensor1";

        public const string VerificarBiometriaUsuarioSensor1 = "controledeacesso26/biometria/verificar/sensor1";
        public const string VerificarBiometriaUsuarioSensor2 = "controledeacesso26/biometria/verificar/sensor2";

        public const string AbrirPorta = "controledeacesso26/biometria/porta/abrir";
    }
}
