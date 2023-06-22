using EsuhaiHRM.Application.Filters;
using System;

namespace EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsByNhanVien
{
    public class GetTimesheetsByNhanVienParameter
    {
        public Guid NhanVienId { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
