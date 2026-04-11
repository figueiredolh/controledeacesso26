using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class SensorOperationCanceledException : ControleDeAcesso26Exception
    {
        public string ErrorMessage { get; set; }
        public SensorOperationCanceledException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
