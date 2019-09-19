using System;
using System.Collections.Generic;

namespace TodoAPI.Models
{
    public partial class TbTodo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
