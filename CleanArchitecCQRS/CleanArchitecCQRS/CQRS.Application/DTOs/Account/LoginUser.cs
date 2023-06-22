using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.DTOs.Account
{
    public class LoginUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid? NhanVienId { get; set; }

    }
}
