using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.DTOs.Account
{
    public class LdapUserInfo
    {
        //string[] requiredAttributes = { "cn", "sn", "uid", "userPrincipalName", "sAMAccountName", "givenName" };
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
