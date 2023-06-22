using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace lab.hangfire.net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : ControllerBase
    {
        /*
        * @description Fire-and-forget Jobs: Các công việc chỉ được thực hiện một lần và gần như ngay lập tức sau khi tạo.
        * @url api/hangfire/fire-and-forget
        */
        [HttpPost]
        [Route("fire-and-forget")]
        public IActionResult FireAndForget(string userName)
        {
            // phương thức Enqueue: thực thi job ngay tức thì
            var jobId = BackgroundJob.Enqueue(() => SayHi(userName));
            return Ok($"Job Id {jobId} đã hoàn tất. Hệ thống đã gửi lời chào đến {userName}!");
        }

        /*
         * @description Delayed Jobs: Các công việc bị trì hoãn cũng chỉ được thực hiện một lần, nhưng không phải ngay lập tức, sau một khoảng thời gian nhất định.
         * @url api/hangfire/delayed
         */
        [HttpPost]
        [Route("delayed")]
        public IActionResult Delayed(string userName)
        {
            // phương thức Schedule: delay 1 khoảng n đơn vị tg, sau đó mới thực thi job
            var jobId = BackgroundJob.Schedule(() => SayHi(userName), TimeSpan.FromSeconds(10));
            return Ok($"Job Id {jobId} đã hoàn tất. Hệ thống đã bị delay khi gửi lời chào đến {userName} trong 10s!");
        }

        /*
         * @description Recurring jobs: Các công việc định kỳ kích hoạt nhiều lần theo lịch trình đã chỉ định.
         * @url api/hangfire/recurring
         */
        [HttpPost]
        [Route("recurring")]
        public IActionResult Recurring(string userName)
        {
            // phương thức AddOrUpdate: job sẽ thực thi định kỳ (recurring) sau 1 khoảng thời gian quy định
            //RecurringJob.AddOrUpdate(() => SayHi(userName), Cron.Minutely);
            //RecurringJob.AddOrUpdate("sayhi", () => SayHi(userName), Cron.Minutely);

            // C3: sử dụng RecurringJobManager là lớp cha của RecurringJob
            var manager = new RecurringJobManager();
            manager.AddOrUpdate("sayhi", Job.FromExpression(() => SayHi(userName)), Cron.Minutely());

            return Ok($"Job định kỳ được lên lịch. Hệ thống sẽ gửi lời chào cho {userName}!");
        }

        /*
         * @description xóa một job định kỳ nếu nó có tồn tại trước đó (truyền vào mã của job đang đc thực thi định kỳ)
         * @url api/hangfire/del-job-recur
         */
        [HttpPost]
        [Route("del-job-recur")]
        public IActionResult DelJobRecurring(string recurringId)
        {
            RecurringJob.RemoveIfExists(recurringId);
            return Ok($"Job định kỳ {recurringId} đã được xóa thành công!");
        }

        /*
         * @description chạy liền lập tức một job định kỳ đã được lên lịch từ trước
         * @url api/hangfire/trigger-job-recur
         */
        [HttpPost]
        [Route("trigger-job-recur")]
        [Obsolete]
        public IActionResult TriggerRecurring(string recurringId)
        {
            RecurringJob.Trigger(recurringId);
            return Ok($"Job định kỳ {recurringId} đã chạy thành công!");
        }

        /*
         * @description Continuations: Các công việc con được thực thi khi công việc mẹ đã kết thúc.
         * @url api/hangfire/continue
         */
        [HttpPost]
        [Route("continue")]
        public IActionResult Unsubscribe(string userName)
        {
            var jobId = BackgroundJob.Enqueue(() => Goodbye(userName));
            var jobContinueId = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine($"Job id {jobId} sent confirm say goodbye to {userName}!"));
            return Ok($"Job id {jobContinueId} has been continued!");
        }

        // api/hangfire/requeue/{jobId}
        [HttpPost]
        [Route("requeue/{jobId}")]
        public IActionResult Unsubscribe(int jobId)
        {
            // chạy lại job được chỉ định
            var isSuccessed = BackgroundJob.Requeue(jobId.ToString());
            return Ok($"Job id {jobId} requeued {isSuccessed}");
        }

        /*
         * @description Function
         */
        public void SayHi(string userName)
        {
            Console.WriteLine($"Hello {userName}, today is {DateTime.Now}");
        }

        public void Goodbye(string userName)
        {
            Console.WriteLine($"Goodbye {userName}!");
        }
    }
}
