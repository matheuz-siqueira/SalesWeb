using Microsoft.AspNetCore.Mvc;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Controllers;

public class SalesRecordsController : Controller
{
    private readonly ISalesRecordsService _salesService;
    public SalesRecordsController(ISalesRecordsService salesService)
    {
        _salesService = salesService; 
    }
    public IActionResult Index()
    {
        return View(); 
    }

    public async Task<ActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
    {
        if(!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 01, 01); 
        }
        if(!maxDate.HasValue)
        {
            maxDate = DateTime.Now; 
        }
        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); 
        var result = await _salesService.FindByDateAsync(minDate, maxDate); 
        return View(result); 
    }

    public IActionResult GroupingSearch()
    {
        return View();
    }
}
