using SalesWeb.Mvc.Models;

namespace SalesWeb.Mvc.Services.Contracts;

public interface ISellerService 
{
    Task<IEnumerable<Seller>> GetAll(); 
    Task Create(Seller seller); 
    Task<Seller> GetById(int id); 
    Task Remove(int id);
}
