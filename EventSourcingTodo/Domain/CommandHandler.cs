using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public interface ICommandHandler
    {
        void Handle(AddTodo c);
        void Handle(RemoveTodo c);
        void Handle(CompleteTodo c);
        void Handle(UncompleteTodo c);
        void Handle(ChangeTodoPosition c);
        void Handle(ChangeTodoDescription c);
    }

    public class CommandHandler : ICommandHandler
    {
        private readonly ITodoListRepository _todoListRepo;

        public CommandHandler(ITodoListRepository todoListRepo)
        {
            _todoListRepo = todoListRepo;
        }

        public void Handle(AddTodo c)
        {
            var todoList = _todoListRepo.Get();
            todoList.Add(c.TodoId, c.Description);
            _todoListRepo.PostChanges(todoList);
        }

        public void Handle(RemoveTodo c)
        {
            var todoList = _todoListRepo.Get();
            todoList.Remove(c.TodoId);
            _todoListRepo.PostChanges(todoList);
        }

        public void Handle(CompleteTodo c)
        {
            var todoList = _todoListRepo.Get();
            todoList.Complete(c.TodoId, c.CompletionTime);
            _todoListRepo.PostChanges(todoList);
        }

        public void Handle(UncompleteTodo c)
        {
            var todoList = _todoListRepo.Get();
            todoList.Uncomplete(c.TodoId);
            _todoListRepo.PostChanges(todoList);
        }

        public void Handle(ChangeTodoPosition c)
        {
            var todoList = _todoListRepo.Get();
            todoList.ChangePosition(c.TodoId, c.Offset);
            _todoListRepo.PostChanges(todoList);
        }

        public void Handle(ChangeTodoDescription c)
        {
            var todoList = _todoListRepo.Get();
            todoList.ChangeDescription(c.TodoId, c.Description);
            _todoListRepo.PostChanges(todoList);
        }
    }
}
