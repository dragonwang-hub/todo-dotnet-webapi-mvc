using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public DateOnly? Expiration { get; set; }
    }
}