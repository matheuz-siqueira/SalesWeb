using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Data;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Services.Contracts;
using SalesWeb.Mvc.Services.Exceptions;

namespace SalesWeb.Mvc.Services;

public class DepartamentService : IDepartamentService
{
    private readonly SalesWebContext _context; 
    public DepartamentService(SalesWebContext context)
    {
        _context = context; 
    }

    public async Task CreateAsync(Departament departament)
    {
        _context.Add(departament); 
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Departament>> GetAll()
    {
        return await _context.Departaments.AsNoTracking()
            .OrderBy(d => d.Name).ToListAsync(); 
    }

    public async Task<Departament> GetByIdAsync(int id)
    {
        return await _context.Departaments.AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task RemoveAsync(int id)
    {
        var departament = await GetByIdAsync(id); 
        _context.Remove(departament); 
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Departament departament)
    {
        bool hasAny = await _context.Departaments.AnyAsync(d => d.Id == departament.Id); 
        if(!hasAny)
        {
            throw new NotFoundException("Departament not found"); 
        }
        try 
        {
            _context.Update(departament); 
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message); 
        }
    }
}
