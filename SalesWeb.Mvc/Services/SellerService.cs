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

    public async Task Create(Seller seller)
    {
        _context.Add(seller); 
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Seller>> GetAll()
    {
        return await _context.Sellers.AsNoTracking().ToListAsync(); 
    }

    public async Task<Seller> GetById(int id)
    {
        return await _context.Sellers.AsNoTracking()
            .FirstOrDefaultAsync(seller => seller.Id == id);
    }

    public async Task Remove(int id)
    {
        var seller = await GetById(id); 
        _context.Remove(seller);  
        await _context.SaveChangesAsync();
    }
}
