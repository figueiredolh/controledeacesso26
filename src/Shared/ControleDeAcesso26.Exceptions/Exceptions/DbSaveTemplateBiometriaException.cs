using ControleDeAcesso26.Exceptions.Base;

namespace ControleDeAcesso26.Exceptions.Exceptions
{
    public class DbSaveTemplateBiometriaException : ControleDeAcesso26Exception
    {
        public string ErrorMessage { get; set; }
        public DbSaveTemplateBiometriaException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
