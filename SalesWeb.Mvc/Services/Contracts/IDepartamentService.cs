using SalesWeb.Mvc.Models;

namespace SalesWeb.Mvc.Services.Contracts;

public interface IDepartamentService
{
    Task<IEnumerable<Departament>> GetAll(); 
    Task CreateAsync(Departament departament);
}
