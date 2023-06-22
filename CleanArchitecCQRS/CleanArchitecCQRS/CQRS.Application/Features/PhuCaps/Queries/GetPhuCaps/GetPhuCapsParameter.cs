using EsuhaiHRM.Application.Filters;

namespace EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCaps
{
    public class GetPhuCapsParameter : RequestParameter
    {
        public int? PhongId { get; set; }
        public int? BanId { get; set; }
    }
}
