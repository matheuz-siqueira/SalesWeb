using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Data;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Services.Contracts;
using SalesWeb.Mvc.Services.Exceptions;

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
            .Include(d => d.Departament)
            .FirstOrDefaultAsync(seller => seller.Id == id);
    }

    public async Task Remove(int id)
    {
        try
        {
            var seller = await GetById(id); 
            _context.Remove(seller);  
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateException)
        {
            throw new IntegrityException("Can't delete seller because he/she has sales"); 
        }
        
    }

    public async Task Update(Seller seller)
    {
        bool hasAny = await _context.Sellers.AnyAsync(x => x.Id == seller.Id); 
        if(!hasAny)
        {
            throw new NotFoundException("Seller not found");
        }
        try
        {
            _context.Update(seller); 
            await _context.SaveChangesAsync();
        } 
        catch(DbUpdateConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }
}
