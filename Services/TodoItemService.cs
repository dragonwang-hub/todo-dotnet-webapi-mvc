using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Dtos;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoItemService
    {
        private readonly TodoItemRepository _repository;

        public TodoItemService(TodoItemRepository repository)
        {
            _repository = repository;
        }

        public List<TodoItemDto> GetAll()
        {
            return _repository.GetAllAsync().GetAwaiter().GetResult().Select(item => ToDto(item)).ToList();
        }

        public TodoItemDto GetById(long id)
        {
            var TodoItem = _repository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (TodoItem is null)
            {
                return null;
            }
            return ToDto(TodoItem);
        }

        public async Task<TodoItem> AddItem(TodoItemDto item)
        {
            var model = ToModel(item);
            await _repository.InsertAsync(model);
            return model;
        }

        public async Task UpdateItem(long id, TodoItemDto dto)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item is null)
            {
                throw new ArgumentException("This item is not exist.");
            }

            var model = ToModel(dto);
            model.Id = id;
            await _repository.UpdateAsync(model);
        }

        public async Task RemoveItem(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item is null)
            {
                throw new ArgumentException("This item is not exist.");
            }
            await _repository.DeleteAsync(item);
        }


        public static TodoItemDto ToDto(TodoItem item)
        {
            return new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                IsCompleted = item.IsCompleted,
                Expiration = item.Expiration
            };
        }

        public static TodoItem ToModel(TodoItemDto item)
        {
            return new TodoItem
            {
                Name = item.Name,
                IsCompleted = item.IsCompleted,
                Expiration = item.Expiration
            };
        }

    }
}