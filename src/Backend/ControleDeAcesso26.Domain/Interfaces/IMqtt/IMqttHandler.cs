namespace ControleDeAcesso26.Domain.Interfaces.IMqtt
{
    public interface IMqttHandler<T>
    {
        public Task<T> HandleMessage(string topic);
    }
}
