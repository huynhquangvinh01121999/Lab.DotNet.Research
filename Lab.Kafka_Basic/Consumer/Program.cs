using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Đọc cấu hình từ appsettings.json
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory()) // Thiết lập đường dẫn gốc cho tệp appsettings.json
                            .AddJsonFile("appsettings.json")
                            .Build();

            var consumer = new KafkaConsumerConfig(configuration);

            // Đăng ký lắng nghe event từ topic
            consumer.Subscribe("test-event");

            while (true)
            {
                try
                {
                    var result = consumer.ConsumerMessage();
                    if (!string.IsNullOrEmpty(result))
                        Console.WriteLine($"Received message: {result}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error consuming message: {e.Error.Reason}");
                }
            }
        }
    }
}
