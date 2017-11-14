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
        private static List<Event> _events = new List<Event>();
        public IList<Event> Events
        {
            get
            {
                lock (_events)
                {
                    return _events;
                }
            }
        }

        public TodoList Get()
        {
            lock (_events)
            {
                return new TodoList(_events);
            }
        }
        
        public void PostChanges(TodoList todoList)
        {
            lock (_events)
            {
                _events.AddRange(todoList.UncommittedChanges);
            }
        }

    }
}