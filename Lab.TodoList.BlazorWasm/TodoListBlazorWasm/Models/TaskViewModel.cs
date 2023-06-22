using System;

namespace TodoListBlazorWasm.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Mode { get; set; }
        public Guid UserId { get; set; }
        public int TypesId { get; set; }
        public string TypesName { get; set; }
    }
}
