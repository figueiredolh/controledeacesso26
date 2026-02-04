using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class ErrorOnValidationException : ControleDeAcesso26Exception
    {
        public readonly List<string> _errorMessages;
        public ErrorOnValidationException(List<string> errorMessages)
        {
            _errorMessages = errorMessages;
        }
    }
}
