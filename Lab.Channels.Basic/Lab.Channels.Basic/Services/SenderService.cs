using Lab.Channels.Basic.Models;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab.Channels.Basic.Services
{
    public class SenderService
    {
        private readonly ChannelWriter<User> _channelWriter;

        public SenderService(ChannelWriter<User> channelWriter)
        {
            _channelWriter = channelWriter;
        }

        public async Task StartSending()
        {
            for (int i = 1; i <= 10; i++)
            {
                var data = new User
                {
                    Id = i,
                    Name = $"User {i}"
                };

                await _channelWriter.WriteAsync(data);
                Console.WriteLine($"Send: {data.Name}");
                await Task.Delay(1000);
            }

            _channelWriter.Complete();
        }
    }
}
