using System;

namespace EsuhaiHRM.Application.Features.Admin.Queries.GetUsers
{
    public class GetUsersByRoleViewModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? NhanVienId { get; set; }
        public string Username { get; set; }
        public string MaNhanVien { get; set; }
    }
}
