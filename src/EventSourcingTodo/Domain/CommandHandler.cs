using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Domain
{
    public class CommandHandler
    {
        public void Handle(AddTodo c)
        {
            var todoList = TodoListRepository.Get();
            todoList.Add(c.TodoId, c.Description);
            TodoListRepository.PostChanges(todoList);
        }

        public void Handle(RemoveTodo c)
        {
            var todoList = TodoListRepository.Get();
            todoList.Remove(c.TodoId);
            TodoListRepository.PostChanges(todoList);
        }

        public void Handle(CompleteTodo c)
        {
            var todoList = TodoListRepository.Get();
            todoList.Complete(c.TodoId, c.CompletionTime);
            TodoListRepository.PostChanges(todoList);
        }

        public void Handle(UncompleteTodo c)
        {
            var todoList = TodoListRepository.Get();
            todoList.Uncomplete(c.TodoId);
            TodoListRepository.PostChanges(todoList);
        }

        public void Handle(ChangeTodoPosition c)
        {
            var todoList = TodoListRepository.Get();
            todoList.ChangePosition(c.TodoId, c.Position);
            TodoListRepository.PostChanges(todoList);
        }

        public void Handle(ChangeTodoDescription c)
        {
            var todoList = TodoListRepository.Get();
            todoList.ChangeDescription(c.TodoId, c.Description);
            TodoListRepository.PostChanges(todoList);
        }
    }
}
