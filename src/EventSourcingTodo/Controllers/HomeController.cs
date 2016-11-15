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
            var viewModel = new IndexViewModel() { TodoList = todoList.Todos };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AddTodoPostModel postModel)
        {
            if (ModelState.IsValid)
            {
                cmdHandler.Handle(new AddTodo(Guid.NewGuid(), postModel.Description));
                return RedirectToAction(nameof(Index));
            }
            var todoList = TodoListRepository.Get();
            var viewModel = new IndexViewModel() { PostModel = postModel, TodoList = todoList.Todos };
            return View(viewModel);
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
