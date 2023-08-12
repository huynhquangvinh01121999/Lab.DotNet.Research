using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAssemblyServer.Entities
{
    public class TodosList : AuditBaseEntity
    {
        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(500)")]
        public string Content { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string Mode { get; set; }    // public/private
    }
}
