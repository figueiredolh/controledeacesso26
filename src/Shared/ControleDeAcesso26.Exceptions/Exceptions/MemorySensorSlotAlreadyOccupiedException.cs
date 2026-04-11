using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class MemorySensorSlotAlreadyOccupiedException : ControleDeAcesso26Exception
    {
        public readonly int IdSensor;
        public readonly string ErrorMessage;
        public MemorySensorSlotAlreadyOccupiedException(int idSensor, string errorMessage)
        {
            IdSensor = idSensor;
            ErrorMessage = errorMessage;
        }
    }
}
