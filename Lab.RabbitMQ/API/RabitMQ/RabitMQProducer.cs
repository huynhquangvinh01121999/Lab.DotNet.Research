using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace API.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void CreateExchange(string exchangeName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "admin",
                Password = "123",
                VirtualHost = "/"
            };

            //Đầu tiên ta tạo mới một connection tới RabbitMQ với username, password và host name.
            var connection = factory.CreateConnection();
            var model = connection.CreateModel();
            
            //Để tạo mới 1 exchange ta dùng hàm ExchangeDeclare với 2 params là exchange name và exchange type
            model.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);
        }

        public void CreateQueue(string queueName, string exchangeName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "admin",
                Password = "123",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // Create Queue
            channel.QueueDeclare(queueName, true, false, false, null);

            // Bind Queue to Exchange
            channel.QueueBind(queueName, exchangeName, "directexchange_key");
        }

        public void SendMessage<T>(T message, string queueName, string exchangeName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5673,
                UserName = "admin",
                Password = "123",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, true, false, false, null);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = false;

            var json = JsonSerializer.Serialize(message);
            var messagebuffer = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: exchangeName, routingKey: "directexchange_key", basicProperties: properties, body: messagebuffer);
        }
    }
}
