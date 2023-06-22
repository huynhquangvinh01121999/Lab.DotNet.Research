using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecurringController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("addNewJob")]
        public IActionResult Post(string jobName, string cronExpression)
        {
            var manager = new RecurringJobManager();
            manager.AddOrUpdate(jobName, () => Console.WriteLine($"Welcome {DateTime.Now.ToString("dd-MM-yyyy")}"), cronExpression);
            return Ok("OK");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatedJob")]
        public IActionResult Put(string jobName, string cronExpression)
        {
            var manager = new RecurringJobManager();
            manager.AddOrUpdate(jobName, () => Console.WriteLine($"Welcome {DateTime.Now.ToString("dd-MM-yyyy")}"), cronExpression);
            return Ok("OK");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("pauseJob/{jobName}")]
        public IActionResult Put(string jobName)
        {
            var manager = new RecurringJobManager();
            manager.AddOrUpdate(jobName,() => Console.WriteLine($"Welcome {DateTime.Now.ToString("dd-MM-yyyy")}"), "* * 31 2 *");
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
