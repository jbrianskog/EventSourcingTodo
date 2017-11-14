using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventSourcingTodo.Models;
using EventSourcingTodo.Domain;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventSourcingTodo.Controllers.Api
{
    // This controller handles all mutations of the domain to reflect the command queue domain model.

    [Route("api/v1/[controller]/[action]", Name = "api_[controller]_[action]")]
    public class CommandController : Controller
    {
        private readonly ICommandHandler _cmdHandler;

        public CommandController(ICommandHandler cmdHandler)
        {
            _cmdHandler = cmdHandler;
        }
        
        [HttpPost]
        public IActionResult AddTodo(AddTodoPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cmdHandler.Handle(new AddTodo(Guid.NewGuid(), postModel.Description));
            return Ok();
        }
        
        [HttpPost]
        public IActionResult RemoveTodo(RemoveTodoPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cmdHandler.Handle(new RemoveTodo(postModel.TodoId));
            return Ok();
        }
        
        [HttpPost]
        public IActionResult CompleteTodo(CompleteTodoPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cmdHandler.Handle(new CompleteTodo(postModel.TodoId, DateTimeOffset.UtcNow));
            return Ok();
        }
        
        [HttpPost]
        public IActionResult UncompleteTodo(UncompleteTodoPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cmdHandler.Handle(new UncompleteTodo(postModel.TodoId));
            return Ok();
        }
        
        [HttpPost]
        public IActionResult ChangeTodoPosition(ChangeTodoPositionPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cmdHandler.Handle(new ChangeTodoPosition(postModel.TodoId, postModel.Offset));
            return Ok();
        }
        
        [HttpPost]
        public IActionResult ChangeTodoDescription(ChangeTodoDescriptionPostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _cmdHandler.Handle(new ChangeTodoDescription(postModel.TodoId, postModel.Description));
            return Ok();
        }
    }
}
