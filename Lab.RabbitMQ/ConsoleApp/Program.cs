using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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

            channel.QueueDeclare("demo_queue", true, false, false, null);

            //Set Event object which listen message from chanel which is sent by producer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = eventArgs.RoutingKey;
                Console.WriteLine($" [x] Received {routingKey}: {message}");
                Console.WriteLine("----------------------------");
            };

            //read the message
            channel.BasicConsume(queue: "demo_queue", autoAck: true, consumer: consumer);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            //Console.ReadKey();
        }
    }
}
