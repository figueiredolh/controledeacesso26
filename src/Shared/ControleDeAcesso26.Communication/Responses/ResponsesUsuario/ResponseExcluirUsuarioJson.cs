namespace ControleDeAcesso26.Communication.Responses.ResponsesUsuario
{
    public class ResponseExcluirUsuarioJson
    {
        public string Nome { get; init; } = string.Empty;
        public string Apelido { get; init; } = string.Empty;
        public bool Ativo { get; init; }
    }
}
