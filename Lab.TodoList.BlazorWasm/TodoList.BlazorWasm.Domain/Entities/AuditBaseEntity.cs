using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.BlazorWasm.Domain.Entities
{
    public abstract class AuditBaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastModifyAt { get; set; }

        public bool? Deleted { get; set; } = false;

    }
}
