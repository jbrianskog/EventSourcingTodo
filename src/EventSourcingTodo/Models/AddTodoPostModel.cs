using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class AddTodoPostModel
    {
        [Required]
        public string Description { get; set; }
    }
}
