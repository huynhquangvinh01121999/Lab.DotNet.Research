using Hangfire;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;

namespace lab.hangfire.net.Jobs
{
    public static class JobServices
    {
        public static void RegisterJobs(this IBackgroundJobClient backgroundJobs)
        {
            /* Cấu hình Cron chạy theo thời gian quy định
                CRON expressions: https://en.wikipedia.org/wiki/Cron#CRON_expression
                *[minute:0-59] *[hour:0-23] *[day:1-31] *[month:1-12] *[dayOfWeek:0-7] *[year(non-required):1970–2099]
             */

            var manager = new RecurringJobManager();

            // chạy vào phút thứ 5 mỗi giờ
            manager.AddOrUpdate("job1", () => Console.WriteLine($"Job chay vao phut thu 5 moi gio, today is {DateTime.Now}"), "5 * * * *");

            // cứ mỗi 1 phút sẽ chạy
            manager.AddOrUpdate("job2", () => Console.WriteLine($"Job chay sau moi 1p, today is {DateTime.Now}"), "*/1 * * * *");

            // chạy vào 10h30 mỗi ngày
            manager.AddOrUpdate("job3", () => Console.WriteLine($"Job chay vao 10h30 moi ngay, today is {DateTime.Now}"), "30 10 * * *");

            // chạy vào ngày 21 mỗi tháng
            manager.AddOrUpdate("job4", () => Console.WriteLine($"Job chay vao ngay 21 moi thang, today is {DateTime.Now}"), "* * 21 * *");

            // chạy vào 10h Thứ Tư
            manager.AddOrUpdate("job5", () => Console.WriteLine($"Job chay vao 10h Thu Tu, today is {DateTime.Now}"), "* 10 * * 3");

            // chạy vào 10h Thứ Sáu, Bảy, CN
            manager.AddOrUpdate("job6", () => Console.WriteLine($"Job chay vao 10h Thu Sau, Bay, CN, today is {DateTime.Now}"), "* 10 * * 5,6,7");

            // chạy vào lúc 10h Thứ Sáu, Bảy, CN từ tháng 8 đến tháng 12
            manager.AddOrUpdate("job7", () => Console.WriteLine($"Job chay vao luc 10h Thu Sau, Bay, CN tu thang 8 den 12, today is {DateTime.Now}"), "* 10 * 8-12 5,6,7");

            // chạy vào cuối mỗi tháng
            manager.AddOrUpdate("job8", () => Console.WriteLine($"Job chay vao cuoi moi thang, today is {DateTime.Now}"), "* * L * *");

            // chạy vào Thứ 6 của tuần thứ 2
            manager.AddOrUpdate("job9", () => Console.WriteLine($"Job chay vao cuoi moi thang, today is {DateTime.Now}"), "* * * * 5#2");

            // cứ mỗi 1 phút gửi mail 1 lần
            manager.AddOrUpdate("sendmail", () => sendMail("Say Hello Everyday"
                                                            , "toEmail@gmail.com", $"Hello Mr.Vinh, today is {DateTime.Now}")
                                                            , "*/1 * * * *");
        }

        public static void SendMail(string subject, string toAddress, string text)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse("fromEmail@email.com");

            email.To.Add(MailboxAddress.Parse(toAddress));

            email.Subject = subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = text;

            email.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTlsWhenAvailable);

                smtp.Authenticate("fromEmail@email.com", "password");

                smtp.Send(email);

                smtp.Disconnect(true);
            }
        }

        public static void sendMail(string subject, string toAddress, string text)
        {
            // Instantiate mimemessage
            var message = new MimeMessage();

            // From address
            message.From.Add(new MailboxAddress("SYSTEM HANGFIRE", "fromEmail@email.com"));

            // To address
            message.To.Add(new MailboxAddress("Vinh Huynh", toAddress));

            // Subject
            message.Subject = subject;

            // Body
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            // configure and send mail
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTlsWhenAvailable);

                smtp.Authenticate("fromEmail@email.com", "password");

                smtp.Send(message);

                smtp.Disconnect(true);
            }
        }
    }
}
