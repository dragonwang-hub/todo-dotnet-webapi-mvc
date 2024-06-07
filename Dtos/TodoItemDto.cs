using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Dtos
{
    public class TodoItemDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateOnly? Expiration { get; set; }
    }
}