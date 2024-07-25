using System.Net;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace AspNetCoreTodo.Controllers.Mvc;

[Authorize]
public class TodoController : Controller
{
    private readonly ITodoItemService _todoItemService;
    private readonly UserManager<IdentityUser> _userManager;

    public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
    {
        _todoItemService = todoItemService;
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) return Challenge();
        // Get to-do items from database
        // Put items into model
        var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);
        
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

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) return Challenge();

        var successful = await _todoItemService.AddItemAsync(newItem, currentUser);
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

        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser == null) return Challenge();
        
        var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
        if (!successful)
        {
            return BadRequest("Could not mark item as done.");
        }
            return RedirectToAction("Index");
    }   

}