using Microsoft.EntityFrameworkCore;
using SalesWeb.Mvc.Models;

namespace SalesWeb.Mvc.Data;

public class SalesWebContext : DbContext
{
    public SalesWebContext(DbContextOptions<SalesWebContext> options) : base (options)
    {}
    public DbSet<Departament> Departaments { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<SalesRecord> SalesRecords { get; set; }
}
