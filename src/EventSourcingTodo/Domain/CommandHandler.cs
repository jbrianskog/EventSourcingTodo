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
        private readonly ITodoListRepository todoListRepo;

        public CommandHandler(ITodoListRepository todoListRepository)
        {
            todoListRepo = todoListRepository;
        }

        public void Handle(AddTodo c)
        {
            var todoList = todoListRepo.Get();
            todoList.Add(c.TodoId, c.Description);
            todoListRepo.PostChanges(todoList);
        }

        public void Handle(RemoveTodo c)
        {
            var todoList = todoListRepo.Get();
            todoList.Remove(c.TodoId);
            todoListRepo.PostChanges(todoList);
        }

        public void Handle(CompleteTodo c)
        {
            var todoList = todoListRepo.Get();
            todoList.Complete(c.TodoId, c.CompletionTime);
            todoListRepo.PostChanges(todoList);
        }

        public void Handle(UncompleteTodo c)
        {
            var todoList = todoListRepo.Get();
            todoList.Uncomplete(c.TodoId);
            todoListRepo.PostChanges(todoList);
        }

        public void Handle(ChangeTodoPosition c)
        {
            var todoList = todoListRepo.Get();
            todoList.ChangePosition(c.TodoId, c.Offset);
            todoListRepo.PostChanges(todoList);
        }

        public void Handle(ChangeTodoDescription c)
        {
            var todoList = todoListRepo.Get();
            todoList.ChangeDescription(c.TodoId, c.Description);
            todoListRepo.PostChanges(todoList);
        }
    }
}
