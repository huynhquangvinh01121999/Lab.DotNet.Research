using Lab.BackgroundTaskWithSignalR.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.BackgroundTaskWithSignalR.BackgroundServices
{
    public class Worker : BackgroundService
    {
        private readonly IHubContext<SignalHub> _hubContext;
        private Timer _timer;

        public Worker(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Da kich hoat cac tac vu nen");

            // Chạy tác vụ nền mỗi 5 giây
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            await Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var message = $"Đây là thông báo từ server lúc {DateTime.Now}";
            _hubContext.Clients.All.SendAsync("ReceiveNotification", message);

            Console.WriteLine("Da gui notification cho client");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Da dung cac tac vu nen");

            // Dừng tác vụ nền khi dừng ứng dụng
            _timer?.Change(Timeout.Infinite, 0);

            await base.StopAsync(stoppingToken);
        }
    }
}
