using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<bool> AddItemAsync(TodoItem newItem);
        Task<TodoItem[]> GetIncompleteItemsAsync();
        Task<bool> MarkDoneAsync(Guid id);

    }
}
