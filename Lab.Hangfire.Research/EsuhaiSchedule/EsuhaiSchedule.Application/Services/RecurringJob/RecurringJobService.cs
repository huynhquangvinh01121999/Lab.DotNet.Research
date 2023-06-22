using EsuhaiSchedule.Application.DTOs;
using EsuhaiSchedule.Application.Enums;
using EsuhaiSchedule.Application.IRepositories;
using EsuhaiSchedule.Application.Services.Email;
using EsuhaiSchedule.Application.Wrappers;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Application.Services.RecurringJob
{
    public class RecurringJobService : IRecurringJobService
    {
        private readonly IConfiguration _configuration;
        private readonly ITongHopDuLieuRepositoryAsync _tongHopDuLieuRepositoryAsync;
        private readonly IEmailServices _emailServices;

        public RecurringJobService(IConfiguration configuration, ITongHopDuLieuRepositoryAsync tongHopDuLieuRepositoryAsync, IEmailServices emailServices)
        {
            _configuration = configuration;
            _tongHopDuLieuRepositoryAsync = tongHopDuLieuRepositoryAsync;
            _emailServices = emailServices;
        }

        public async Task S2_SendMail_TongHopXetDuyetC1(int nam, int thang)
        {
            try
            {
                var tonghopxetduyets = await _tongHopDuLieuRepositoryAsync.S2_Job_GetTongHopXetDuyetC1(nam, thang);
                if (tonghopxetduyets != null)
                {
                    foreach (var item in tonghopxetduyets)
                    {
                        string path = Path.Combine(_configuration["MailTemplate:FilePath"]);
                        using (StreamReader SourceReader = File.OpenText(path))
                        {
                            string html = SourceReader.ReadToEnd();
                            var body = string.Format(html, item.HoTen, item.SLDonDieuChinh, item.SLDonTangCa, item.SLDonPhuCap
                                                        , item.HoTen, item.SLDonDieuChinh, item.SLDonTangCa, item.SLDonPhuCap);

                            var tongDonXetduyet = item.SLDonDieuChinh + item.SLDonTangCa + item.SLDonPhuCap;
                            var request = new EmailDtos
                            {
                                //To = item.EmailCongTy,
                                To = "nhatnam@gmail.com",
                                Subject = $"Anh/Chị có {tongDonXetduyet} yêu cầu phê duyệt trên Timesheet/ タイムシートの承認リクエストが {tongDonXetduyet}件あります。",
                                Body = body
                            };

                            await _emailServices.SendAsync(request);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task S2_SendMail_TongHopXetDuyetC2(int nam, int thang)
        {
            try
            {
                var tonghopxetduyets = await _tongHopDuLieuRepositoryAsync.S2_Job_GetTongHopXetDuyetC2(nam, thang);
                if (tonghopxetduyets != null)
                {
                    foreach (var item in tonghopxetduyets)
                    {
                        string path = Path.Combine(_configuration["MailTemplate:FilePath"], _configuration["MailTemplate:FileName"]);
                        using (StreamReader SourceReader = File.OpenText(path))
                        {
                            string html = SourceReader.ReadToEnd();
                            var body = string.Format(html, item.HoTen, item.SLDonDieuChinh, item.SLDonTangCa, item.SLDonPhuCap
                                                        , item.HoTen, item.SLDonDieuChinh, item.SLDonTangCa, item.SLDonPhuCap);

                            var tongDonXetduyet = item.SLDonDieuChinh + item.SLDonTangCa + item.SLDonPhuCap;
                            var request = new EmailDtos
                            {
                                //To = item.EmailCongTy,
                                To = "nhatnam@esuhai.com",
                                Subject = $"Anh/Chị có {tongDonXetduyet} yêu cầu phê duyệt trên Timesheet/ タイムシートの承認リクエストが {tongDonXetduyet}件あります。",
                                Body = body
                            };

                            await _emailServices.SendAsync(request);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response<Task> S2_AddOrUpdateRecurringJob(AddOrUpdateDtos request)
        {
            try
            {
                var cronExpression = CronExpression(request);
                var manager = new RecurringJobManager();

                switch (request.StoredType)
                {
                    case JobEnums.TongHopXetDuyetC1:
                        manager.AddOrUpdate(request.StoredType.ToString(), () => S2_SendMail_TongHopXetDuyetC1(DateTime.Now.Year, DateTime.Now.Month), cronExpression);
                        break;

                    case JobEnums.TongHopXetDuyetC2:
                        manager.AddOrUpdate(request.StoredType.ToString(), () => S2_SendMail_TongHopXetDuyetC2(DateTime.Now.Year, DateTime.Now.Month), cronExpression);
                        break;
                }
                return new Response<Task>(null, "Updated successfully!");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*
         * @params CronTypeRequest
         * @return string
         * @description Ham xu ly Cron Expression
         */
        private string CronExpression(AddOrUpdateDtos cronType)
        {
            string minute = "*"
                , hour = "*"
                , day = "*"
                , month = "*"
                , dayOfWeek = "*";
            try
            {
                #region  start Xu ly phut
                if (cronType.IsRunAfterMinute && cronType.Minutes > 0)
                    minute = $"*/{cronType.Minutes}";

                if(!cronType.IsRunAfterMinute && cronType.Minutes > 0)
                    minute = $"{cronType.Minutes}";
                #endregion

                #region start Xu ly gio
                if (cronType.IsRunAfterHour && cronType.Hours > 0)
                    hour = $"*/{cronType.Hours}";

                if (!cronType.IsRunAfterHour && cronType.Hours > 0)
                    hour = $"{cronType.Hours}";
                #endregion

                #region Xu ly ngay
                if (cronType.IsRunAfterDay && cronType.Days > 0)
                    day = $"*/{cronType.Days}";

                if (!cronType.IsRunAfterDay && cronType.Days > 0)
                    day = $"{cronType.Days}";
                #endregion

                #region Xu ly thang
                if (cronType.IsRunAfterMonth && cronType.Months > 0)
                    month = $"*/{cronType.Months}";

                if (!cronType.IsRunAfterMonth && cronType.Months > 0)
                    month = $"{cronType.Months}";
                #endregion

                #region Xu ly ngay trong tuan
                if (((decimal)cronType.DayofWeeks) > 0)
                    dayOfWeek = $"{((decimal)cronType.DayofWeeks)}";
                #endregion

                var cronResult = $"{minute} {hour} {day} {month} {dayOfWeek}";
                return cronResult;
            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message); 
            }
        }
    }
}
