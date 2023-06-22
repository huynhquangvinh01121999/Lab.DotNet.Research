using Application.DTOs.Email;
using Application.Features.Emails;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Client.JobHandle
{
    public static class JobServices
    {
        public static void RegisterJobs(this IRecurringJobManager recurringJobManager, IApplicationBuilder app)
        {
            //recurringJobManager.AddOrUpdate<HangFireActiveJob>(nameof(HangFireActiveJob),
            //    job => app.ApplicationServices.GetRequiredService<IHangFireActiveJob>().Run(JobCancellationToken.Null), "*/1 * * * *");

            var request = new EmailRequest
            {
                To = "hqvinh99@gmail.com",
                Subject = "Thông báo nhận quà ngày Tết 2023",
                Body = $"Hello Mr.Vinh, today is <b>{DateTime.Now.ToString("dd/MM/yyyy")}</b>"
            };

            recurringJobManager.AddOrUpdate<EmailServices>(nameof(EmailServices),
                job => app.ApplicationServices.GetRequiredService<IEmailServices>().SendAsync(request), "*/1 * * * *");

            // de disable job: ta setting cron la mot gia tri khong ton tai trong thuc te nhung van dung quy tac cron
            //recurringJobManager.AddOrUpdate<EmailServices>(nameof(EmailServices),
            //    job => app.ApplicationServices.GetRequiredService<IEmailServices>().SendAsync(request), "* * 31 2 *");
        }
    }
}
