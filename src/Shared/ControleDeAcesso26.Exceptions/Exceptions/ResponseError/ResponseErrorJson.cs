namespace ControleDeAcesso26.Exceptions.Exceptions.ResponseError
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; }
        public ResponseErrorJson(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
