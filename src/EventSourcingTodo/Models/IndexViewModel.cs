using EventSourcingTodo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class IndexViewModel
    {
        public AddTodoPostModel PostModel { get; set; }
        public IEnumerable<Todo> TodoList { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
