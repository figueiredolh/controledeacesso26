using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class AttemptLimitReachedException : ControleDeAcesso26Exception
    {
        public readonly string ErrorMessage;
        public AttemptLimitReachedException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
