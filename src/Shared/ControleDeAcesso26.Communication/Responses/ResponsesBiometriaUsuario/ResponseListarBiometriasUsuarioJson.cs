namespace ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario
{
    public class ResponseListarBiometriasUsuarioJson
    {
        public string NomeUsuario { get; set; } = null!;
        public int IdSensor1 { get; init; }
        public int? IdSensor2 { get; init; }
    }
}
