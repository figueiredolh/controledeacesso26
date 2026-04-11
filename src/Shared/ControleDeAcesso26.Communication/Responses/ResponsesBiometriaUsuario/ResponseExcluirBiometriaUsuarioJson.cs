namespace ControleDeAcesso26.Communication.Responses.ResponsesBiometriaUsuario
{
    public class ResponseExcluirBiometriaUsuarioJson
    {
        public required string NomeUsuario { get; init; }
        public required int IdSensor1 { get; init; }
        public int? IdSensor2 { get; init; }
        public string Status { get; init; } = "Template excluído";
    }
}
