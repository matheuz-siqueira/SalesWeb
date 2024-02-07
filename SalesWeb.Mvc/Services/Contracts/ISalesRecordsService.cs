using SalesWeb.Mvc.Models;

namespace SalesWeb.Mvc.Services.Contracts;

public interface ISalesRecordsService
{
    Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
}
