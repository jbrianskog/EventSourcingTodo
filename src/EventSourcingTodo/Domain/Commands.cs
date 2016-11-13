using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public class AddTodo
    {
        public readonly Guid TodoId;
        public readonly string Description;
        public AddTodo(Guid todoId, string description)
        {
            TodoId = todoId;
            Description = description;
        }
    }

    public class RemoveTodo
    {
        public readonly Guid TodoId;
        public RemoveTodo(Guid todoId)
        {
            TodoId = todoId;
        }
    }

    public class CompleteTodo
    {
        public readonly Guid TodoId;
        public CompleteTodo(Guid todoId)
        {
            TodoId = todoId;
        }
    }

    public class UncompleteTodo
    {
        public readonly Guid TodoId;
        public UncompleteTodo(Guid todoId)
        {
            TodoId = todoId;
        }
    }

    public class ChangeTodoPosition
    {
        public readonly Guid TodoId;
        public readonly int Position;
        public ChangeTodoPosition(Guid todoId, int position)
        {
            TodoId = todoId;
            Position = position;
        }
    }

    public class ChangeTodoDescription
    {
        public readonly Guid TodoId;
        public readonly string Description;
        public ChangeTodoDescription(Guid todoId, string description)
        {
            TodoId = todoId;
            Description = description;
        }
    }
}
