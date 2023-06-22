using System;
using System.Collections.Generic;

namespace EsuhaiHRM.Domain.Users
{
    public class UserActivity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string Email { get; set; }
        public string ControllerName { get; set; }
        public string ControllerAction { get; set; }
        public string ControllerFunction { get; set; }
        public string ControllerRole { get; set; }
        public bool? IsSuccess { get; set; }
        public string DataBefore { get; set; }
        public string DataAfter { get; set; }
        public string DataKey { get; set; }
        public DateTime ActivityDate { get; set; } = DateTime.Now;
    }
}
