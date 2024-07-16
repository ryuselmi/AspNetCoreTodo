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
}