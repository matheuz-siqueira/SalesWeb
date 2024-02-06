using SalesWeb.Mvc.Models;
using SalesWeb.Mvc.Models.Enums;

namespace SalesWeb.Mvc.Data;

public class SeedingService
{
    private readonly SalesWebContext _context;
    public SeedingService(SalesWebContext context)
    {
        _context = context; 
    }

    public void Seed()
    {
        if(_context.Departaments.Any() || _context.Sellers.Any() || _context.SalesRecords.Any())
            return; 
        
        Departament d1 = new(1, "Computer");
        Departament d2 = new(2, "Electronics");
        Departament d3 = new(3, "Fashion"); 
        Departament d4 = new(4, "Books");
        
        Seller s1 = new(1, "Bob Brown", "bob@mail.com", new DateTime(1998, 4, 21), 1000.0M, d1);
        Seller s2 = new(2, "Maria Green", "maria@mail.com", new DateTime(1979, 12, 31), 3500.0M, d2);
        Seller s3 = new(3, "Alex Grey", "alex@mail.com", new DateTime(1988, 1, 15), 2200.0M, d1);
        Seller s4 = new(4, "Martha Red", "martha@mail.com", new DateTime(1993, 11, 30), 3000.0M, d4);
        Seller s5 = new(5, "Donald Blue", "donald@mail.com", new DateTime(2000, 1, 9), 4000.0M, d3);
        Seller s6 = new(6, "Robert Black", "robert@mail.com", new DateTime(1997, 3, 4), 3000.0M, d2);

        SalesRecord sr1 = new(1, new DateTime(2023, 09, 25), 11000.0M, SaleStatus.Billed, s1);
        SalesRecord sr2 = new(2, new DateTime(2023, 09, 4), 7000.0M, SaleStatus.Billed, s1);
        SalesRecord sr3 = new(3, new DateTime(2023, 09, 13), 4000.0M, SaleStatus.Canceled, s2);
        SalesRecord sr4 = new(4, new DateTime(2023, 09, 1), 8000.0M, SaleStatus.Billed, s4);
        SalesRecord sr5 = new(5, new DateTime(2023, 09, 21), 3000.0M, SaleStatus.Billed, s5);
        SalesRecord sr6 = new(6, new DateTime(2023, 09, 15), 2000.0M, SaleStatus.Billed, s2);
        SalesRecord sr7 = new(7, new DateTime(2023, 09, 28), 13000.0M, SaleStatus.Billed, s2);
        SalesRecord sr8 = new(8, new DateTime(2023, 09, 11), 4000.0M, SaleStatus.Billed, s3);
        SalesRecord sr9 = new(9, new DateTime(2023, 09, 14), 11000.0M, SaleStatus.Pending, s6);
        SalesRecord sr10 = new(10, new DateTime(2023, 09, 7), 9000.0M, SaleStatus.Billed, s6);
        SalesRecord sr11 = new(11, new DateTime(2023, 09, 13), 6000.0M, SaleStatus.Billed, s1);
        SalesRecord sr12 = new(12, new DateTime(2023, 09, 25), 7000.0M, SaleStatus.Pending, s1);
        SalesRecord sr13 = new(13, new DateTime(2023, 09, 29), 10000.0M, SaleStatus.Billed, s3);
        SalesRecord sr14 = new(14, new DateTime(2023, 09, 4), 3000.0M, SaleStatus.Billed, s4);
        SalesRecord sr15 = new(15, new DateTime(2023, 09, 12), 3000.0M, SaleStatus.Billed, s2);
        SalesRecord sr16 = new(16, new DateTime(2023, 10, 5), 2000.0M, SaleStatus.Billed, s5);
        SalesRecord sr17 = new(17, new DateTime(2023, 10, 1), 12000.0M, SaleStatus.Canceled, s2);
        SalesRecord sr18 = new(18, new DateTime(2023, 10, 24), 6000.0M, SaleStatus.Billed, s5);
        SalesRecord sr19 = new(19, new DateTime(2023, 10, 22), 8000.0M, SaleStatus.Billed, s1);
        SalesRecord sr20 = new(20, new DateTime(2023, 09, 15), 3000.0M, SaleStatus.Canceled, s3);

        _context.Departaments.AddRange(d1, d2, d3, d4); 
        _context.Sellers.AddRange(s1, s2, s3, s4, s5, s6);
        _context.SalesRecords.AddRange(sr1, sr2, sr3, sr4, sr5, sr6,
            sr7, sr8, sr9, sr10, sr11, sr12, sr13, sr14, sr15, sr16,
            sr17, sr18, sr19, sr20); 

        _context.SaveChanges();
    }
}
