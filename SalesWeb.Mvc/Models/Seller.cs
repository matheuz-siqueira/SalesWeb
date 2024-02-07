using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace SalesWeb.Mvc.Models;

public class Seller : BaseEntity
{
    public Seller()
    {}
    public Seller(int id, string name, string email, 
        DateTime birthDate, decimal baseSalary, Departament departament)
    {
        Id = id; 
        Name = name; 
        Email = email; 
        BirthDate = birthDate; 
        BaseSalary = baseSalary; 
        Departament = departament;
    }


    public string Name { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    [Display(Name = "Base Salary")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public decimal BaseSalary { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

    public Departament Departament { get; set; }
    public int DepartamentId { get; set; }

    public void AddSales(SalesRecord sr)
    {
        Sales.Add(sr); 
    }

    public void RemoveSales(SalesRecord sr)
    {
        Sales.Remove(sr);
    }
    public decimal TotalSales(DateTime initial, DateTime final)
    {
        return Sales.Where(sr => sr.Date >= initial && sr.Date <= final)
            .Sum(sr => sr.Amount); 
    }
}
