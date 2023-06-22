using Grpc.Net.Client;
using GrpcClient;
using System;
using System.Threading.Tasks;

namespace GrpcProductClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var productId = 1;
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new Product.ProductClient(channel);

                while (productId <= 3)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();

                    var response = await client.GetProductDetailAsync(new ProductDetailRequest { ProductId = productId });
                    productId = productId == 3 ? 1 : productId + 1;

                    Console.WriteLine($"Product detail: {response}");
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
