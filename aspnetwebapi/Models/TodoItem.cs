using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspnetwebapi.Models
{
    public  class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
    }
    public class TodoItemViewModel{
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
