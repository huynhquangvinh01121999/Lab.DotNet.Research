using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var count = 1;
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new Greeter.GreeterClient(channel);

                while (true)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();

                    var replyHello = await client.SayHelloAsync(new HelloRequest { Name = $"Huynh Quang Vinh {count}" });
                    var replyGoodbye = await client.SayGoodbyeAsync(new GoodbyeRequest { Name = $"Huynh Quang Vinh {count}", Age = 18 });
                    count++;

                    Console.WriteLine($"Greetings: {replyHello}");
                    Console.WriteLine($"Greetings: {replyGoodbye}");
                    Console.ReadLine();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}
