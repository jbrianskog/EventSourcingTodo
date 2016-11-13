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

        public static TodoList Get()
        {
            lock (events)
            {
                return new TodoList(events);
            }
        }
        
        public static void PostChanges(TodoList todoList)
        {
            lock (events)
            {
                events.AddRange(todoList.UncommittedChanges);
            }
        }
    }
}