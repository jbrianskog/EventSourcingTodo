using EventSourcingTodo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class TodoListPartialViewModel
    {
        public IEnumerable<Todo> TodoList { get; set; }
    }
}
