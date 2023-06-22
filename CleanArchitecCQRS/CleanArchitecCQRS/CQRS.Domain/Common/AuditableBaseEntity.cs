using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        //public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? Deleted { get; set; }
    }
}
