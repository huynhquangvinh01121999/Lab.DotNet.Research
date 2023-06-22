using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.WebApi.Models
{
    public class Role
    {
        public const string SUPERADMIN_ADMIN = "SuperAdmin,Admin";
        public const string SUPERADMIN = "SuperAdmin";
        public const string ADMIN = "Admin";
        public const string BASIC = "Basic";
        
        //public const string ADMIN = "Admin";

        public const string USER = "User";
        public const string User_C1C2 = "User_c1c2";
        public const string USER_HR = "User_Hr";

        public const string HRM_VIEW = "HRM_View";
        public const string HRM_ADD = "HRM_Add";
        public const string HRM_EDIT = "HRM_Edit";
        public const string HRM_DELETE = "HRM_Delete";

        public const string DBNS_VIEW = "DBNS_View";
        public const string DBNS_ADD = "DBNS_Add";
        public const string DBNS_EDIT = "DBNS_Edit";
        public const string DBNS_DELETE = "DBNS_Delete";

    }
}
