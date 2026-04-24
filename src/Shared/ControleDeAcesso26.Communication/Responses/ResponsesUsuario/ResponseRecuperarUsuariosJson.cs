using System.Security;

namespace ControleDeAcesso26.Communication.Responses.ResponsesUsuario
{
    public class ResponseRecuperarUsuariosJson
    {
        public long Id { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Apelido { get; init; } = string.Empty;
        public DateTime DataCriacao { get; init; }
        public bool Ativo { get; init; }
    }
}
