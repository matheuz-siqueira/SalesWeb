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
        if(ModelState.IsValid)
        {
            await _sellerService.Create(seller); 
            return RedirectToAction(nameof(Index)); 
        }
        return View("Error"); 
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if(id is null)
        {
            return NotFound(); 
        }
        var seller = await _sellerService.GetById(id.Value); 
        if(seller is null)
        {
            return NotFound(); 
        }
        return View(seller); 
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        await _sellerService.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if(id is null)
        {
            return NotFound(); 
        }
        var seller = await _sellerService.GetById(id.Value); 
        if(seller is null)
        {
            return NotFound(); 
        } 
        return View(seller); 
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if(id is null)
        {
            return NotFound(); 
        }
        var seller = await _sellerService.GetById(id.Value); 
        if(seller is null)
        {
            return NotFound(); 
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
            return View("Error"); 
        }
        if(id != seller.Id) 
        {
            return BadRequest(); 
        }

        try
        {
            await _sellerService.Update(seller); 
            return RedirectToAction(nameof(Index));
        }
        catch(NotFoundException )
        {
            return NotFound();
        }
        catch(DbConcurrencyException)
        {
            return BadRequest(); 
        }
    }

}
