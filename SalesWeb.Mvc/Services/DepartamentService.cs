using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Data;
using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Services.Contracts;

namespace SalesWeb.Mvc.Services;

public class DepartamentService : IDepartamentService
{
    private readonly SalesWebContext _context; 
    public DepartamentService(SalesWebContext context)
    {
        _context = context; 
    }
    public async Task<IEnumerable<Departament>> GetAll()
    {
        return await _context.Departaments.AsNoTracking()
            .OrderBy(d => d.Name).ToListAsync(); 
    }
}
