using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class RemoveTodoPostModel
    {
        [Required]
        public Guid TodoId { get; set; }
    }
}
