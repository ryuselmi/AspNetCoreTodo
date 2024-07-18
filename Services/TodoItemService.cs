using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
public class TodoItemService : ITodoItemService
{
    private readonly ApplicationDbContext _dbContext;
    public TodoItemService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AddItemAsync(TodoItem newItem)
    {
        newItem.Id = Guid.NewGuid();
        newItem.IsDone = false;
        newItem.DueAt = DateTimeOffset.Now.AddDays(3);

        _dbContext.Items.Add(newItem);

        return await _dbContext.SaveChangesAsync() == 1;

    }

    public async Task<TodoItem[]> GetIncompleteItemsAsync()
    {
        return await _dbContext.Items
            .Where(x => x.IsDone == false)
            .ToArrayAsync();
        }
    }
}