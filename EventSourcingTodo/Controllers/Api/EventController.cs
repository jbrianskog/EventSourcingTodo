using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventSourcingTodo.Domain;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventSourcingTodo.Controllers.Api
{
    [Route("api/v1/[controller]", Name = "api_[controller]_[action]")]
    public class EventController : Controller
    {
        private readonly ITodoListRepository _todoListRepo;

        public EventController(ITodoListRepository todoListRepo)
        {
            _todoListRepo = todoListRepo;
        }

        [HttpGet("")]
        public IEnumerable<Event> GetAll()
        {
            return _todoListRepo.Events;
        }
    }
}
