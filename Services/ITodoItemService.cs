using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<bool> AddItemAsync(TodoItem newItem, IdentityUser currentUser);
        Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser currentUser);
        Task<bool> MarkDoneAsync(Guid id, IdentityUser currentUser);

    }
}
