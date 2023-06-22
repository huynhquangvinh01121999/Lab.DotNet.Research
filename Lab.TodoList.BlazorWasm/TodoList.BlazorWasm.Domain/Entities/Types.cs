using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.BlazorWasm.Domain.Entities
{
    public class Types : AuditBaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public IList<TodosList> Todos { get; set; }
    }
}
