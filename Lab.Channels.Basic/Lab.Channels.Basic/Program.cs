using Lab.Channels.Basic.Models;
using Lab.Channels.Basic.Services;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab.Channels.Basic
{
    class Program
    {
        //static async Task Main()
        //{
        //    // Tạo một channel kiểu int để truyền dữ liệu giữa các luồng
        //    var channel = Channel.CreateUnbounded<int>();

        //    // Luồng gửi dữ liệu
        //    await Task.Run(async () =>
        //    {
        //        for (int i = 1; i <= 10; i++)
        //        {
        //            // Gửi giá trị vào channel
        //            await channel.Writer.WriteAsync(i);
        //            Console.WriteLine($"Send: {i}");
        //            await Task.Delay(100); // Đợi 0,1 giây trước khi gửi giá trị tiếp theo
        //        }

        //        // Đánh dấu là không còn giá trị để gửi
        //        channel.Writer.Complete();
        //    });

        //    // Luồng nhận dữ liệu
        //    await Task.Run(async () =>
        //    {
        //        // Đọc dữ liệu từ channel cho đến khi không còn giá trị để nhận
        //        while (await channel.Reader.WaitToReadAsync())
        //        {
        //            // Nhận giá trị từ channel
        //            while (channel.Reader.TryRead(out var value))
        //            {
        //                Console.WriteLine($"Receive: {value}");
        //            }
        //        }
        //    });

        //    Console.ReadLine();
        //}

        static async Task Main()
        {
            var channel = Channel.CreateUnbounded<User>();

            var senderService = new SenderService(channel.Writer);
            var receiverService = new ReceiverService(channel.Reader);

            var sendTask = senderService.StartSending();
            var receiveTask = receiverService.StartReceiving();

            await Task.WhenAll(sendTask, receiveTask);

            Console.ReadLine();
        }
    }
}
