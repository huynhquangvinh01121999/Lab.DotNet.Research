using EsuhaiSchedule.Application.Services.RecurringJob;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EsuhaiSchedule.API.Jobs
{
    public static class JobTongHopXetDuyet
    {
        public static void RegisterJobTongHopXetDuyet(this IRecurringJobManager recurringJobManager, IApplicationBuilder app)
        {
            recurringJobManager.AddOrUpdate<RecurringJobService>("TongHopXetDuyetC1",
                job => app.ApplicationServices.GetRequiredService<IRecurringJobService>().S2_SendMail_TongHopXetDuyetC1(DateTime.Now.Year, DateTime.Now.Month), "*/1 * * * *");
            
            recurringJobManager.AddOrUpdate<RecurringJobService>("TongHopXetDuyetC2",
                job => app.ApplicationServices.GetRequiredService<IRecurringJobService>().S2_SendMail_TongHopXetDuyetC2(DateTime.Now.Year, DateTime.Now.Month), "*/1 * * * *");
        }
    }
}
