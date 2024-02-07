using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Models.ViewModels;
using SalesWeb.Mvc.Services.Contracts;
using SalesWeb.Mvc.Services.Exceptions;

namespace SalesWeb.Mvc.Controllers;

public class SellersController : Controller
{
    private readonly ISellerService _sellerService; 
    private readonly IDepartamentService _departamentService;
    public SellersController(ISellerService sellerService, IDepartamentService departamentService)
    {
        _sellerService = sellerService; 
        _departamentService = departamentService; 
    }
    public async Task<IActionResult> Index()
    {
        var list = await _sellerService.GetAll();
        return View(list);
    }

    public async Task<ActionResult> Create()
    {
        var departaments = await _departamentService.GetAll(); 
        var viewModel = new SellerFormViewModel { Departaments = departaments }; 
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Seller seller)
    {
        if(!ModelState.IsValid)
        {
            var departaments = await _departamentService.GetAll();
            var viewModel = new SellerFormViewModel {  Seller = seller, Departaments = departaments };
            return View(viewModel); 
        }
        await _sellerService.Create(seller); 
        return RedirectToAction(nameof(Index)); 

    }

    public async Task<IActionResult> Delete(int? id)
    {
        if(id is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Invalid request" });
        }
        var seller = await _sellerService.GetById(id.Value); 
        if(seller is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Seller not found" }); 
        }
        return View(seller); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try 
        {
            await _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        catch(IntegrityException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
        }
        
    }

    public async Task<IActionResult> Details(int? id)
    {
        if(id is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Invalid request" });
        }
        var seller = await _sellerService.GetById(id.Value); 
        if(seller is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Seller not found" });
        } 
        return View(seller); 
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if(id is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Invalid request" }); 
        }
        var seller = await _sellerService.GetById(id.Value); 
        if(seller is null)
        {
            return RedirectToAction(nameof(Error), new { message = "Seller not found" });
        }
        List<Departament> departaments = (List<Departament>)await _departamentService.GetAll(); 
        SellerFormViewModel viewModel = new() { Seller = seller, Departaments = departaments}; 
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, Seller seller)
    {
        if(!ModelState.IsValid)
        {
            var departaments = await _departamentService.GetAll();
            var viewModel = new SellerFormViewModel {  Seller = seller, Departaments = departaments };
            return View(viewModel); 
        }
        if(id != seller.Id) 
        {
            return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); 
        }

        try
        {
            await _sellerService.Update(seller); 
            return RedirectToAction(nameof(Index));
        }
        catch(NotFoundException e)
        {
            return RedirectToAction(nameof(Error), new { message = e.Message });
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
