namespace ProductServices.RabbitMqService
{
    public interface IMessageProducer
    {
        public void SendingMessage<T>(T message);
    }
}
