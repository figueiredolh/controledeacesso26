namespace ControleDeAcesso26.Domain.Interfaces.IMqtt
{
    public interface IMqttPublisher<T>
    {
        public Task PublishMessage(string topic, T payload);
    }
}
