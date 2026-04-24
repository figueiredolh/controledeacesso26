using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class SensorDeleteTemplateException : ControleDeAcesso26Exception
    {
        public readonly int IdSensor;
        public string ErrorMessage { get; set; }
        public SensorDeleteTemplateException(int idSensor, string errorMessage)
        {
            IdSensor = idSensor;
            ErrorMessage = errorMessage;
        }
    }
}
