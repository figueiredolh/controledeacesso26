using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class NotFoundException : ControleDeAcesso26Exception
    {
        public readonly string ErrorMessage;
        public NotFoundException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
