using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.BlazorWasm.Domain.Entities
{
    public class AppUsers : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Dob { get; set; }

        public IList<TodosList> Todos { get; set; }
    }
}
