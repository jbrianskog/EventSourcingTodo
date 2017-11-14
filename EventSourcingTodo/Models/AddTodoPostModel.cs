using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class AddTodoPostModel
    {
        [Required(ErrorMessage = "You need to name your to-do")]
        public string Description { get; set; }
    }
}
