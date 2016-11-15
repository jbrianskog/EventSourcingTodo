using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public class TodoListRepository
    {
        // Global event stream for single global TodoList. Replace with something like Event Store.
        private static List<Event> events = new List<Event>();
        public static IEnumerable<Event> Events { get { return events; } }

        public static TodoList Get()
        {
            return new TodoList(events);
        }
        
        public static void PostChanges(TodoList todoList)
        {
            events.AddRange(todoList.UncommittedChanges);
        }

    }
}