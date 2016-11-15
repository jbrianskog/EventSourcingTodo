using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class UncompleteTodoPostModel
    {
        [Required]
        public Guid TodoId { get; set; }
    }
}
