using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public abstract class AggregateRoot
    {
        public AggregateRoot(IEnumerable<Event> history)
        {
            foreach (var e in history)
            {
                Apply(e);
            }
        }

        private readonly List<Event> _changes = new List<Event>();
        public IEnumerable<Event> UncommittedChanges { get { return _changes; } }

        protected abstract void Apply(Event e);
        protected void ApplyAndStageNewEvent(Event e)
        {
            Apply(e);
            _changes.Add(e);
        }
    }

    // root
    public class TodoList : AggregateRoot
    {
        public TodoList(IEnumerable<Event> history) : base(history)
        {
        }

        // Currently, nothing reads this, so the Apply()s that mutate it are pointless, but
        // I want to treat this as an exercise with the assumption that the list is
        // needed to e.g. perform command validation.
        private List<Todo> todos = new List<Todo>();
        public IEnumerable<Todo> Todos { get { return todos; } }

        public void Add(Guid todoId, string description)
        {
            ApplyAndStageNewEvent(new TodoAdded(todoId, description));
        }
        
        public void Remove(Guid todoId)
        {
            ApplyAndStageNewEvent(new TodoRemoved(todoId));
        }
        
        public void Complete(Guid todoId, DateTimeOffset completionTime)
        {
            ApplyAndStageNewEvent(new TodoCompleted(todoId, completionTime));
        }

        public void Uncomplete(Guid todoId)
        {
            ApplyAndStageNewEvent(new TodoUncompleted(todoId));
        }

        public void ChangePosition(Guid todoId, int position)
        {
            ApplyAndStageNewEvent(new TodoPositionChanged(todoId, position));
        }
        
        public void ChangeDescription(Guid todoId, string description)
        {
            ApplyAndStageNewEvent(new TodoDescriptionChanged(todoId, description));
        }

        protected override void Apply(Event e)
        {
            Apply(e as dynamic);
        }

        private void Apply(TodoAdded e)
        {
            todos.Add(new Todo()
            {
                Id = e.TodoId,
                Description = e.Description,
                IsCompleted = false
            });
        }

        private void Apply(TodoRemoved e)
        {
            todos.RemoveAll(x => x.Id == e.TodoId);
        }

        private void Apply(TodoCompleted e)
        {
            var todo = todos.FirstOrDefault(x => x.Id == e.TodoId);
            if (todo != null)
            {
                todo.IsCompleted = true;
                todo.CompletionTime = e.CompletionTime;
            }
        }

        private void Apply(TodoUncompleted e)
        {
            var todo = todos.FirstOrDefault(x => x.Id == e.TodoId);
            if (todo != null)
            {
                todo.IsCompleted = false;
            }
        }

        private void Apply(TodoPositionChanged e)
        {
            var from = todos.FindIndex(x => x.Id == e.TodoId);
            if (from == -1)
            {
                return;
            }
            var to = from + e.Offset;
            if (to < 0)
            {
                to = 0;
            }
            else if (to > todos.Count - 1)
            {
                to = todos.Count - 1;
            }
            var tmp = todos[from];
            todos.RemoveAt(from);
            todos.Insert(to, tmp);
        }

        private void Apply(TodoDescriptionChanged e)
        {
            var todo = todos.FirstOrDefault(x => x.Id == e.TodoId);
            if (todo != null)
            {
                todo.Description = e.Description;
            }
        }
    }

    public class Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset CompletionTime { get; set; }
    }
}
