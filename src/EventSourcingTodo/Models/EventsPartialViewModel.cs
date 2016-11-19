using EventSourcingTodo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class EventsPartialViewModel
    {
        public IList<Event> Events { get; set; }
    }
}
