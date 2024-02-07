namespace SalesWeb.Mvc.Models.ViewModels;

public class SellerFormViewModel
{
    public Seller Seller { get; set; }
    public IEnumerable<Departament> Departaments { get; set; }
}
