using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class ErrorOnValidationException : ControleDeAcesso26Exception
    {
        public readonly List<string> ErrorMessages;
        public ErrorOnValidationException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
