namespace API.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void CreateExchange(string exchangeName);
        public void CreateQueue(string queueName, string exchangeName);
        public void SendMessage<T>(T message, string queueName, string exchangeName);
    }
}
