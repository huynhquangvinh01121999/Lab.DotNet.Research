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
            // Đọc cấu hình từ appsettings.json
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory()) // Thiết lập đường dẫn gốc cho tệp appsettings.json
                            .AddJsonFile("appsettings.json")
                            .Build();


            var producer = new KafkaProducerConfig(configuration);

            // Nhập dữ liệu từ bàn phím và gửi nó đến Kafka Producer
            while (true)
            {
                Console.Write("Input message (Press Enter to sent event or press double Enter to exit): ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Gửi message đến Kafka
                    await producer.ProduceMessage("test-event", input);
                    Console.WriteLine("Send successfully!!!");
                }
                else
                {
                    // Thoát khỏi vòng lặp
                    break;
                }
            }

            producer.Dispose();
        }
    }
}
