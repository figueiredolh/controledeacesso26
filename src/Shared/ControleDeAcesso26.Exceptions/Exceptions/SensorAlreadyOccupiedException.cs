using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class SensorAlreadyOccupiedException : ControleDeAcesso26Exception
    {
        public readonly string ErrorMessage;
        public SensorAlreadyOccupiedException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
