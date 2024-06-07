using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoItemRepository
    {
        private readonly TodoItemContext _context;

        public TodoItemRepository(TodoItemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task InsertAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}