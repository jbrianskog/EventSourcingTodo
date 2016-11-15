using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public abstract class Event
    {
    }

    public class TodoAdded : Event
    {
        public readonly Guid TodoId;
        public readonly string Description;
        public TodoAdded(Guid todoId, string description)
        {
            TodoId = todoId;
            Description = description;
        }
    }

    public class TodoRemoved : Event
    {
        public readonly Guid TodoId;
        public TodoRemoved(Guid todoId)
        {
            TodoId = todoId;
        }
    }

    public class TodoCompleted : Event
    {
        public readonly Guid TodoId;
        public readonly DateTimeOffset CompletionTime;
        public TodoCompleted(Guid todoId, DateTimeOffset completionTime)
        {
            TodoId = todoId;
            CompletionTime = completionTime;
        }
    }

    public class TodoUncompleted : Event
    {
        public readonly Guid TodoId;
        public TodoUncompleted(Guid todoId)
        {
            TodoId = todoId;
        }
    }

    public class TodoPositionChanged : Event
    {
        public readonly Guid TodoId;
        public readonly int Position;
        public TodoPositionChanged(Guid todoId, int position)
        {
            TodoId = todoId;
            Position = position;
        }
    }

    public class TodoDescriptionChanged : Event
    {
        public readonly Guid TodoId;
        public readonly string Description;
        public TodoDescriptionChanged(Guid todoId, string description)
        {
            TodoId = todoId;
            Description = description;
        }
    }
}