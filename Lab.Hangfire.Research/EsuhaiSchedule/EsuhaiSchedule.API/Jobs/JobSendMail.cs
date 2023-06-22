using EsuhaiSchedule.Application.DTOs;
using EsuhaiSchedule.Application.Services.Email;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EsuhaiSchedule.API.Jobs
{
    public static class JobSendMail
    {
        public static void RegisterJobSendMail(this IRecurringJobManager recurringJobManager, IApplicationBuilder app)
        {
            var request = new EmailDtos
            {
                To = "nhatnam@esuhai.com",
                Subject = "Anh/Chị có 20 yêu cầu phê duyệt trên Timesheet/ タイムシートの承認リクエストが 20件あります。"
            };

            recurringJobManager.AddOrUpdate<EmailServices>(nameof(EmailServices),
                job => app.ApplicationServices.GetRequiredService<IEmailServices>().SendAsync(request), "*/1 * * * *");
        }
    }
}
