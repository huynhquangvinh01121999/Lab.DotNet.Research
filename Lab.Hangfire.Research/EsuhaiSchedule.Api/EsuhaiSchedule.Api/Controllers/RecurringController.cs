using EsuhaiSchedule.Services.DTOs.Email;
using EsuhaiSchedule.Services.Interfaces;
using EsuhaiSchedule.Services.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecurringController : ControllerBase
    {
        private readonly IEmailServices _emailServices;

        public RecurringController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        [HttpPost("addNewJob")]
        public async Task<IActionResult> Post(string jobName, string cronExpression)
        {
            var request = new EmailRequest
            {
                To = "hqvinh99@gmail.com",
                Subject = "Say Hello Everyday",
                Body = $"Hello Mr.Vinh, today is {DateTime.Now.ToString("dd/MM/yyyy")}"
            };

            var manager = new RecurringJobManager();
            manager.AddOrUpdate<EmailServices>(jobName,
                job => _emailServices.SendAsync(request), cronExpression);
            return Ok("OK");
        }

        [HttpPut("updatedJob")]
        public async Task<IActionResult> Put(string jobName, string cronExpression)
        {
            var request = new EmailRequest
            {
                To = "hqvinh99@gmail.com",
                Subject = "Say Hello Everyday",
                Body = $"Hello Mr.Vinh, today is {DateTime.Now.ToString("dd/MM/yyyy")}"
            };

            var manager = new RecurringJobManager();
            manager.AddOrUpdate<EmailServices>(jobName,
                job => _emailServices.SendAsync(request), cronExpression);
            return Ok("OK");
        }

        [HttpPut("pauseJob/{jobName}")]
        public async Task<IActionResult> Put(string jobName)
        {
            var request = new EmailRequest
            {
                To = "hqvinh99@gmail.com",
                Subject = "Say Hello Everyday",
                Body = $"Hello Mr.Vinh, today is {DateTime.Now.ToString("dd/MM/yyyy")}"
            };

            var manager = new RecurringJobManager();
            manager.AddOrUpdate<EmailServices>(jobName,
                job => _emailServices.SendAsync(request), "* * 31 2 *");
            return Ok($"Job {jobName} đã được tạm dừng thành công!");
        }

        [HttpDelete("{jobName}")]
        public IActionResult Delete(string jobName)
        {
            RecurringJob.RemoveIfExists(jobName);
            return Ok($"Job {jobName} đã được xóa thành công!");
        }
    }
}
