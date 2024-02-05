using SalesWeb.Mvc.Models.Enums;

namespace SalesWeb.Mvc.Models;

public class SalesRecord : BaseEntity
{
    public SalesRecord()
    {}

    public SalesRecord(int id, DateTime date, decimal amount, 
        SaleStatus saleStatus, Seller seller)
    {
        Id = id; 
        Date = date;  
        Amount = amount;
        SaleStatus = saleStatus;        
        Seller = seller;
    }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; } 
    public SaleStatus SaleStatus { get; set; }

    public Seller Seller { get; set; }
    public int SellerId { get; set; }
}
