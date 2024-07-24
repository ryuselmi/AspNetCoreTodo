using System.Net;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace AspNetCoreTodo.Controllers.Mvc;

public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;

    public TodoController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }
    public async Task<IActionResult> Index()
    {
        // Get to-do items from database
        // Put items into model
        var items = await _todoItemService.GetIncompleteItemsAsync();
        
        var model = new TodoViewModel()
        {
            Items = items
        };

        // Render view using model
        return View(model);
    }
    
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(TodoItem newItem)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        var successful = await _todoItemService.AddItemAsync(newItem);
        if (!successful)
        {
            return BadRequest("Could not add item.");
        }

        return RedirectToAction("Index");
    }
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkDone(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToAction("Index");
        }
        
        var successful = await _todoItemService.MarkDoneAsync(id);
        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }
            return RedirectToAction("Index");
    }   

}