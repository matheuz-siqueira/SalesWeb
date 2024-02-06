using Microsoft.AspNetCore.Mvc;

namespace SalesWeb.Mvc.Controllers;

public class SellersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
