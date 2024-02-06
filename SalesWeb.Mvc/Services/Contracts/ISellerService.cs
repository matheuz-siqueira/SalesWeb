using SalesWeb.Mvc.Models;

namespace SalesWeb.Mvc.Services.Contracts;

public interface ISellerService 
{
    Task<IEnumerable<Seller>> GetAll(); 
}
