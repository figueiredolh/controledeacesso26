using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class DbDeleteUsuarioException : ControleDeAcesso26Exception
    {
        public string ErrorMessage { get; set; }
        public DbDeleteUsuarioException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
