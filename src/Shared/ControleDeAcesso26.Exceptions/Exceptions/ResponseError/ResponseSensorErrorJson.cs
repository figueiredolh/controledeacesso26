namespace ControleDeAcesso26.Exceptions.Exceptions.ResponseError
{
    public class ResponseSensorErrorJson : ResponseErrorJson
    {
        public int IdSensor { get; set; } 
        public ResponseSensorErrorJson(int idSensor, string errorMessage) : base(errorMessage)
        {
            IdSensor = idSensor;
        }
    }
}
