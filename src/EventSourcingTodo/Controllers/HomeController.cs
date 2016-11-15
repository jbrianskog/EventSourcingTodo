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
        private static readonly CommandHandler cmdHandler = new CommandHandler();

        public IActionResult Index()
        {
            var todoList = TodoListRepository.Get();
            var viewModel = new IndexViewModel()
            {
                TodoList = todoList.Todos,
                Events = TodoListRepository.Events
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTodo(AddTodoPostModel addTodoPostModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new AddTodo(Guid.NewGuid(), addTodoPostModel.Description));
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new IndexViewModel()
            {
                AddTodoPostModel = addTodoPostModel,
                TodoList = TodoListRepository.Get().Todos,
                Events = TodoListRepository.Events
            };
            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveTodo(RemoveTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new RemoveTodo(postModel.TodoId));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteTodo(CompleteTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new CompleteTodo(postModel.TodoId, DateTimeOffset.UtcNow));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UncompleteTodo(UncompleteTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new UncompleteTodo(postModel.TodoId));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeTodoPosition(ChangeTodoPositionPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new ChangeTodoPosition(postModel.TodoId, postModel.Offset));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeTodoDescription(ChangeTodoDescriptionPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new ChangeTodoDescription(postModel.TodoId, postModel.Description));
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new IndexViewModel()
            {
                ChangeTodoDescriptionPostModel = postModel,
                TodoList = TodoListRepository.Get().Todos,
                Events = TodoListRepository.Events
            };
            return View("Index", viewModel);
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
