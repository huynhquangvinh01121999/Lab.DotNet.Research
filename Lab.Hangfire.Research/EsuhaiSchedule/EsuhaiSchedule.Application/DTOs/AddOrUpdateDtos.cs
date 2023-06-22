using EsuhaiSchedule.Application.Enums;

namespace EsuhaiSchedule.Application.DTOs
{
    public class AddOrUpdateDtos
    {
        public JobEnums StoredType { get; set; }

        public bool IsRunAfterMinute { get; set; }
        public int Minutes { get; set; }

        public bool IsRunAfterHour { get; set; }
        public int Hours { get; set; }

        public bool IsRunAfterDay { get; set; }
        public int Days { get; set; }

        public bool IsRunAfterMonth { get; set; }
        public int Months { get; set; }

        public DayofWeekEnums DayofWeeks { get; set; }
    }
}
