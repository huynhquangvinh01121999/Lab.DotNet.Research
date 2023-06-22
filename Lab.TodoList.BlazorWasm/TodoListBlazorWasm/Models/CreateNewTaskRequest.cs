using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListBlazorWasm.Models
{
    public class CreateNewTaskRequest
    {
        [MaxLength(20, ErrorMessage = "Not be than 20 character!")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Not be than 100 character!")]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "EndDate is required")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Mode is required")]
        public string Mode { get; set; } = "public";

        public Guid UserId { get; set; } = new Guid("8D571F54-4C55-4C9C-800E-8A6433F0A4FF");

        [Required(ErrorMessage = "TypeId is required")]
        public int TypeId { get; set; }
    }
}
