using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Models;

namespace SalesWeb.Mvc.Data;

public class SalesWebContext : DbContext
{
    public SalesWebContext(DbContextOptions<SalesWebContext> options) : base (options)
    {}
    DbSet<Departament> Departaments { get; set; }
}
