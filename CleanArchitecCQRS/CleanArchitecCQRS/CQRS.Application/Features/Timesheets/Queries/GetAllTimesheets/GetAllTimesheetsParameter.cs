using EsuhaiHRM.Application.Filters;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetAllTimesheets
{
    public class GetAllTimesheetsParameter : RequestParameter
    {
        public string SearchValue { get; set; }
    }
}
