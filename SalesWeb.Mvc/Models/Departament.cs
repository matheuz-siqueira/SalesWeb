using System.ComponentModel.DataAnnotations;

namespace SalesWeb.Mvc.Models;

public class Departament : BaseEntity
{
    public Departament() {}

    public Departament(int id, string name)
    {
        Id = id; 
        Name = name;
    }

    [Required(ErrorMessage = "{0} is required")] 
    [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
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
