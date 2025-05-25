using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Producer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            //// Đọc cấu hình từ appsettings.json
            //var configuration = new ConfigurationBuilder()
            //                .SetBasePath(Directory.GetCurrentDirectory()) // Thiết lập đường dẫn gốc cho tệp appsettings.json
            //                .AddJsonFile("appsettings.json")
            //                .Build();


            //var producer = new KafkaProducerConfig(configuration);

            //// Nhập dữ liệu từ bàn phím và gửi nó đến Kafka Producer
            //while (true)
            //{
            //    Console.Write("Input message (Press Enter to sent event or press double Enter to exit): ");
            //    string input = Console.ReadLine();

            //    if (!string.IsNullOrWhiteSpace(input))
            //    {
            //        // Gửi message đến Kafka
            //        await producer.ProduceMessage("test-event", input);
            //        Console.WriteLine("Send successfully!!!");
            //    }
            //    else
            //    {
            //        // Thoát khỏi vòng lặp
            //        break;
            //    }
            //}

            //producer.Dispose();


            var config = new ProducerConfig
            {
                BootstrapServers = "192.168.2.185:9092",
                ClientId = "my-producer", // ID của client
                Acks = Acks.All // Xác nhận của Kafka, có thể là 'All', 'None', hoặc số lượng brokers cần xác nhận
            };

            // Tạo producer
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                try
                {
                    var topic = "my-topic"; // Tên của topic bạn muốn gửi message đến

                    // Gửi message đến Kafka topic
                    var deliveryReport = await producer.ProduceAsync(topic, new Message<string, string> { Key = "key", Value = "Hello Kafka" });

                    // In ra thông tin về message đã gửi (tùy chọn)
                    Console.WriteLine($"Delivered message '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
                }
                catch (ProduceException<string, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
