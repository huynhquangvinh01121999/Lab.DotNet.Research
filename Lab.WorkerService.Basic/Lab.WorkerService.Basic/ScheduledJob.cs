using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.WorkerService.Basic
{
    public class ScheduledJob : BackgroundService
    {
        private readonly ILogger<ScheduledJob> _logger;

        public ScheduledJob(ILogger<ScheduledJob> logger)
        {
            _logger = logger;
        }

        // chạy job vào 10 giờ sáng mỗi ngày
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Đặt lịch trình cho công việc chạy ở đây
                // Ví dụ: Chạy mỗi ngày vào lúc 10 giờ sáng
                var now = DateTime.Now;
                var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 10, 0, 0);

                if (now >= scheduledTime)
                {
                    // Thực hiện công việc ở đây
                    _logger.LogInformation("Running scheduled job...");

                    // Đợi một khoảng thời gian để tránh chạy công việc nhiều lần
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                else
                {
                    // Chờ đến thời gian kế tiếp
                    var delay = scheduledTime - now;
                    await Task.Delay(delay, stoppingToken);
                }
            }
        }

        #region
        // chạy job vào 10h sáng thứ 6 mỗi tuần
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        var now = DateTime.Now;

        //        // Tìm thời điểm kế tiếp của thứ 6 10h sáng
        //        var nextFriday = now.AddDays(((int)DayOfWeek.Friday - (int)now.DayOfWeek + 7) % 7);
        //        var scheduledTime = new DateTime(nextFriday.Year, nextFriday.Month, nextFriday.Day, 10, 0, 0);

        //        if (now >= scheduledTime)
        //        {
        //            // Thực hiện công việc ở đây
        //            _logger.LogInformation("Running scheduled job...");

        //            // Đợi một khoảng thời gian để tránh chạy công việc nhiều lần
        //            await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
        //        }
        //        else
        //        {
        //            // Chờ đến thời gian kế tiếp
        //            var delay = scheduledTime - now;
        //            await Task.Delay(delay, stoppingToken);
        //        }
        //    }
        //}

        // chạy job vào ngày cuối tháng
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        var now = DateTime.Now;

        //        // Tìm thời điểm kế tiếp của ngày cuối cùng trong tháng
        //        var lastDayOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        //        var scheduledTime = new DateTime(lastDayOfMonth.Year, lastDayOfMonth.Month, lastDayOfMonth.Day, 10, 0, 0);

        //        if (now >= scheduledTime)
        //        {
        //            // Thực hiện công việc ở đây
        //            _logger.LogInformation("Running scheduled job...");

        //            // Đợi một khoảng thời gian để tránh chạy công việc nhiều lần
        //            var nextMonth = now.AddMonths(1);
        //            var nextScheduledTime = new DateTime(nextMonth.Year, nextMonth.Month, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month), 10, 0, 0);
        //            var delay = nextScheduledTime - now;
        //            await Task.Delay(delay, stoppingToken);
        //        }
        //        else
        //        {
        //            // Chờ đến thời gian kế tiếp
        //            var delay = scheduledTime - now;
        //            await Task.Delay(delay, stoppingToken);
        //        }
        //    }
        //}
        #endregion
    }
}
