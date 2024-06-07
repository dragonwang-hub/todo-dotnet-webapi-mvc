using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Dtos;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoItemService _service;

        public TodoItemController(TodoItemService service)
        {
            _service = service;
        }

        // GET: api/TodoItem
        [HttpGet]
        public ActionResult<List<TodoItemDto>> GetTodoItems()
        {
            return _service.GetAll();
        }

        // GET: api/TodoItem/5
        [HttpGet("{id}")]
        public ActionResult<TodoItemDto> GetTodoItem(long id)
        {
            var todoItem = _service.GetById(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDto todoItem)
        {
            try
            {
                await _service.UpdateItem(id, todoItem);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/TodoItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostTodoItem(TodoItemDto todoItemDto)
        {
            var todoItem = await _service.AddItem(todoItemDto);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _service.RemoveItem(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
