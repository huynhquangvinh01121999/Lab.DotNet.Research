using System;

namespace TodoList.BlazorWasm.Application.Features.Todos.Commands.CreateTodos
{
    public class CreateTodosViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Mode { get; set; }    // public/private
        public Guid UserID { get; set; }
    }
}
