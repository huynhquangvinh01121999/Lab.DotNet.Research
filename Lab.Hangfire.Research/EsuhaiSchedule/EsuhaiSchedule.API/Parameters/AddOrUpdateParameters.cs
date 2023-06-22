using EsuhaiSchedule.Application.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiSchedule.API.Parameters
{
    public class AddOrUpdateParameters
    {
        /// <summary>
        /// Tên job
        /// </summary>
        [Required]
        public JobEnums StoredType { get; set; }

        /// <summary>
        /// chọn true  -> job sẽ chạy sau mỗi n phút
        /// chọn false -> job lặp tại phút chỉ định
        /// </summary>
        public bool IsRunAfterMinute { get; set; }

        /// <summary>
        /// [Min = 0, Max = 60]
        /// NULL -> Lặp sau mỗi phút
        /// </summary>
        [Range(0, 60)]
        public int Minutes { get; set; }

        /// <summary>
        /// chọn true  -> job sẽ chạy sau mỗi n giờ
        /// chọn false -> job lặp tại giờ chỉ định
        /// </summary>
        public bool IsRunAfterHour { get; set; }

        /// <summary>
        /// [Min = 0, Max = 23]
        /// NULL -> Lặp sau mỗi giờ
        /// </summary>
        [Range(0, 23)]
        public int Hours { get; set; }

        /// <summary>
        /// chọn true  -> job sẽ chạy sau mỗi n ngày
        /// chọn false -> job lặp tại ngày chỉ định
        /// </summary>
        public bool IsRunAfterDay { get; set; }

        /// <summary>
        /// [Min = 1, Max = 12]
        /// NULL -> Lặp sau mỗi ngày
        /// </summary>
        [Range(0, 31)]
        public int Days { get; set; }

        /// <summary>
        /// chọn true  -> job sẽ chạy sau mỗi n tháng
        /// chọn false -> job lặp tại tháng chỉ định
        /// </summary>
        public bool IsRunAfterMonth { get; set; }

        /// <summary>
        /// [Min = 1, Max = 12]
        /// NULL -> Lặp sau mỗi tháng
        /// </summary>
        [Range(0, 12)]
        public int Months { get; set; }

        // dayofWeeks
        [Required]
        public DayofWeekEnums DayofWeeks { get; set; }
    }
}
