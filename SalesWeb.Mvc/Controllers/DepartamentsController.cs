using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Models.ViewModels;
using SalesWeb.Mvc.Services.Contracts;
using SalesWeb.Mvc.Services.Exceptions;


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

    public async Task<IActionResult> Edit(int? id) 
    {
        if(id is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Invalid request"}); 
        }
        var departament = await _departamentService.GetByIdAsync(id.Value);
        if(departament is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Departament not found" });
        }
        return View(departament); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int? id, Departament departament)
    {
        if(!ModelState.IsValid)
        {
            return View(departament); 
        }   
        if(id.Value != departament.Id) 
        {
            return RedirectToAction(nameof(Error), new { message = "Invalid request" }); 
        }
        try
        {
            await _departamentService.UpdateAsync(departament); 
            return RedirectToAction(nameof(Index));
        }
        catch(DbConcurrencyException e) 
        {
            return RedirectToAction(nameof(Error), new { message = e.Message }); 
        }
    }

    public IActionResult Error(string message)
    {
        var viewModel = new ErrorViewModel
        {
            Message = message,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(viewModel); 
    }
}
