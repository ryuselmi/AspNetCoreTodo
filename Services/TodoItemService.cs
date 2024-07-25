using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
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

    public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser currentUser)
    {
        newItem.Id = Guid.NewGuid();
        newItem.IsDone = false;
        newItem.DueAt = DateTimeOffset.Now.AddDays(3);
        newItem.UserID = currentUser.Id;

        _dbContext.Items.Add(newItem);

        return await _dbContext.SaveChangesAsync() == 1;

    }

    public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser currentUser)
    {
        return await _dbContext.Items
            .Where(x => x.IsDone == false && x.UserID == currentUser.Id)
            .ToArrayAsync();
    }

    public async Task<bool> MarkDoneAsync(Guid id, IdentityUser currentUser)
    {
        var item = await _dbContext.Items
            .Where(x => x.Id == id && x.UserID == currentUser.Id)
            .SingleOrDefaultAsync();

        if (item == null) return false;

        item.IsDone = true;

        var saveResult = await _dbContext.SaveChangesAsync();
        return saveResult == 1; // One entity should have been updated
    }


    
    }
}