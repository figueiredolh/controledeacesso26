namespace ControleDeAcesso26.Communication.Responses.ResponsesUsuario
{
    public class ResponseCriarUsuarioJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}
