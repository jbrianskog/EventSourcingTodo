﻿using EventSourcingTodo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingTodo.Models
{
    public class IndexViewModel
    {
        public AddTodoPostModel AddTodoPostModel { get; set; }
        public ChangeTodoDescriptionPostModel ChangeTodoDescriptionPostModel { get; set; }
        public TodoListPartialViewModel TodoListPartialViewModel { get; set; }
        public EventsPartialViewModel EventsPartialViewModel { get; set; }
    }
}
