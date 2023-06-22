using EsuhaiSchedule.Services.DTOs.Email;
using EsuhaiSchedule.Services.Interfaces;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Services.Repositories
{
    public class HangFireActiveJob : IHangFireActiveJob
    {
        private readonly IServiceProvider _serviceProvider;

        public HangFireActiveJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task Run(IJobCancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            await RunAtTimeOf();
        }

        public async Task RunAtTimeOf()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var emailServices = scope.ServiceProvider.GetRequiredService<IEmailServices>();
            await emailServices.SendAsync(new EmailRequest
            {
                To = "hqvinh99@gmail.com",
                Subject = "Say Hello Everyday",
                Body = $"Hello Mr.Vinh, today is {DateTime.Now}"
            });
        }
    }
}
