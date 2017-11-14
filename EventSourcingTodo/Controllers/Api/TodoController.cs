using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventSourcingTodo.Domain;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventSourcingTodo.Controllers.Api
{
    // This only has REST actions for reading entities because all writes are handled as
    // commands in the CommandController. This reflects the command queue nature of the domain model.

    [Route("api/v1/[controller]", Name = "api_[controller]_[action]")]
    public class TodoController : Controller
    {
        private readonly ITodoListRepository _todoListRepo;

        public TodoController(ITodoListRepository todoListRepo)
        {
            _todoListRepo = todoListRepo;
        }

        [HttpGet("")]
        public IEnumerable<Todo> GetAll()
        {
            return _todoListRepo.Get().Todos;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var todo = _todoListRepo.Get().GetTodo(id);
            if (todo == null)
            {
                return NotFound();
            }
            return new ObjectResult(todo);
        }
    }
}
