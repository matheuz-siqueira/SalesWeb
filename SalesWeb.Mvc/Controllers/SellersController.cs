using Microsoft.AspNetCore.Mvc;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Controllers;

public class SellersController : Controller
{
    private readonly ISellerService _service; 
    public SellersController(ISellerService service)
    {
        _service = service; 
    }
    public async Task<IActionResult> Index()
    {
        var list = await _service.GetAll();
        return View(list);
    }
}
