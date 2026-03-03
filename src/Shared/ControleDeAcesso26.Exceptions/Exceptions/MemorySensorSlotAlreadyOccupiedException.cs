using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class MemorySensorSlotAlreadyOccupiedException : ControleDeAcesso26Exception
    {
        public readonly string ErrorMessage;
        public MemorySensorSlotAlreadyOccupiedException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
