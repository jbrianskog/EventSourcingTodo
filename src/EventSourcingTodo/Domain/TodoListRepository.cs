using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public interface ITodoListRepository
    {
        IList<Event> Events { get; }
        TodoList Get();
        void PostChanges(TodoList todoList);
    }

    public class TodoListRepository : ITodoListRepository
    {
        // Global event stream for single global TodoList. Replace with something like Event Store.
        private List<Event> events = new List<Event>();
        public IList<Event> Events
        {
            get
            {
                lock (events)
                {
                    return events;
                }
            }
        }

        public TodoList Get()
        {
            lock (events)
            {
                return new TodoList(events);
            }
        }
        
        public void PostChanges(TodoList todoList)
        {
            lock (events)
            {
                events.AddRange(todoList.UncommittedChanges);
            }
        }

    }
}