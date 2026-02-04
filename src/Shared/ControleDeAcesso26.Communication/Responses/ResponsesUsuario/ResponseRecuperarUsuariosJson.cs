using System.Security;

namespace ControleDeAcesso26.Communication.Responses.ResponsesUsuario
{
    public class ResponseRecuperarUsuariosJson
    {
        public string Nome { get; set; } = string.Empty;
        public string Apelido { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
