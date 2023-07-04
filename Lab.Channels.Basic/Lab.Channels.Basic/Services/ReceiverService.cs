using Lab.Channels.Basic.Models;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab.Channels.Basic.Services
{
    public class ReceiverService
    {
        private readonly ChannelReader<User> _channelReader;

        public ReceiverService(ChannelReader<User> channelReader)
        {
            _channelReader = channelReader;
        }

        public async Task StartReceiving()
        {
            while (await _channelReader.WaitToReadAsync())
            {
                while (_channelReader.TryRead(out var data))
                {
                    Console.WriteLine($"Receive: {data.Name}");
                }
            }
        }
    }
}
