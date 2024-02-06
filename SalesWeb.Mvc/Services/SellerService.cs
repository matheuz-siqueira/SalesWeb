using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Data;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Services;

public class SellerService : ISellerService
{
    private readonly SalesWebContext _context; 
    public SellerService(SalesWebContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<Seller>> GetAll()
    {
        return await _context.Sellers.AsNoTracking().ToListAsync(); 
    }
}
