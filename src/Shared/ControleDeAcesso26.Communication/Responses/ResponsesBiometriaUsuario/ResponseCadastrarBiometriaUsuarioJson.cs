namespace ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario
{
    public class ResponseCadastrarBiometriaUsuarioJson
    {
        public required string NomeUsuario { get; init; }
        public string? UsuarioTemplate { get; init; }
        public required int IdSensor { get; init; }
        public string? Status { get; init; }
    }
}
