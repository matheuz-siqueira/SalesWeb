namespace SalesWeb.Mvc.Models;

public class Departament : BaseEntity
{
    public Departament() {}

    public Departament(int id, string name)
    {
        Id = id; 
        Name = name;
    }

    public string Name { get; set; }
    public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    public void AddSeller(Seller seller)
    {
        Sellers.Add(seller);
    }

    public decimal TotalSales(DateTime initial, DateTime final)
    {
        return Sellers.Sum(seller => seller.TotalSales(initial, final)); 
    }
}
