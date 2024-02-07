using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Data;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Services;

public class SalesRecordsService : ISalesRecordsService
{
    private readonly SalesWebContext _context;
    public SalesRecordsService(SalesWebContext context)
    {
        _context = context; 
    }

    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from obj in _context.SalesRecords select obj; 
        if(minDate.HasValue)
        {
            result = result.Where(x => x.Date >= minDate.Value); 
        }
        if(maxDate.HasValue)
        {
            result = result.Where(x => x.Date <= maxDate.Value); 
        }
        return await result
            .Include(s => s.Seller)
            .Include(s => s.Seller.Departament)
            .OrderByDescending(d => d.Date)
            .ToListAsync();  
    }
}
