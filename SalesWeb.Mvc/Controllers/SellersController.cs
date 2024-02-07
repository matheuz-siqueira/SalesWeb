using Microsoft.AspNetCore.Mvc;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Models.ViewModels;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Controllers;

public class SellersController : Controller
{
    private readonly ISellerService _service; 
    private readonly IDepartamentService _departamentService;
    public SellersController(ISellerService service, IDepartamentService departamentService)
    {
        _service = service; 
        _departamentService = departamentService; 
    }
    public async Task<IActionResult> Index()
    {
        var list = await _service.GetAll();
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
            await _service.Create(seller); 
            return RedirectToAction(nameof(Index)); 
        }
        return View("Error"); 
    }
}
