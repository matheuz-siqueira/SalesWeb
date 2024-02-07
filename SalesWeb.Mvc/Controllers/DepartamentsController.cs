using Microsoft.AspNetCore.Mvc;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Controllers;

public class DepartamentsController : Controller
{
    private readonly IDepartamentService _departamentService;
    public DepartamentsController(IDepartamentService departamentService)
    {
        _departamentService = departamentService; 
    }

    public async Task<IActionResult> Index()
    {
        var departaments = await _departamentService.GetAll(); 
        return View(departaments); 
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Departament departament)
    {
        if(!ModelState.IsValid)
        {
            return View(departament); 
        }
        await _departamentService.CreateAsync(departament); 
        return RedirectToAction(nameof(Index)); 
    }

    
}
