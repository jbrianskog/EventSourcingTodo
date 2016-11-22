using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventSourcingTodo.Models;
using EventSourcingTodo.Domain;

namespace EventSourcingTodo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandHandler _cmdHandler;
        private readonly ITodoListRepository _todoListRepo;

        public HomeController(ICommandHandler cmdHandler, ITodoListRepository todoListRepo)
        {
            _cmdHandler = cmdHandler;
            _todoListRepo = todoListRepo;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                TodoListPartialViewModel = todoListPartialViewModel(),
                EventsPartialViewModel = new EventsPartialViewModel() { Events = _todoListRepo.Events }
            };
            return View(viewModel);
        }

        private TodoListPartialViewModel todoListPartialViewModel()
        {
            return new TodoListPartialViewModel()
            {
                TodoList = _todoListRepo.Get().Todos.Where(x => !x.IsCompleted),
                CompletedTodoList = _todoListRepo.Get().Todos.Where(x => x.IsCompleted).Reverse()
            };
        }

        public IActionResult Events()
        {
            return PartialView("_EventsPartial", new EventsPartialViewModel() { Events = _todoListRepo.Events });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTodo(AddTodoPostModel addTodoPostModel)
        {
            if (ModelState.IsValid)
            {
                _cmdHandler.Handle(new AddTodo(Guid.NewGuid(), addTodoPostModel.Description));
            }
            return PartialView("_TodoListPartial", todoListPartialViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveTodo(RemoveTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                _cmdHandler.Handle(new RemoveTodo(postModel.TodoId));
            }
            return PartialView("_TodoListPartial", todoListPartialViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteTodo(CompleteTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                _cmdHandler.Handle(new CompleteTodo(postModel.TodoId, DateTimeOffset.UtcNow));
            }
            return PartialView("_TodoListPartial", todoListPartialViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UncompleteTodo(UncompleteTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                _cmdHandler.Handle(new UncompleteTodo(postModel.TodoId));
            }
            return PartialView("_TodoListPartial", todoListPartialViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeTodoPosition(ChangeTodoPositionPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                _cmdHandler.Handle(new ChangeTodoPosition(postModel.TodoId, postModel.Offset));
            }
            return PartialView("_TodoListPartial", todoListPartialViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeTodoDescription(ChangeTodoDescriptionPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                _cmdHandler.Handle(new ChangeTodoDescription(postModel.TodoId, postModel.Description));
            }
            return PartialView("_TodoListPartial", todoListPartialViewModel());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
