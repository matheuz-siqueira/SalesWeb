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

    [Required(ErrorMessage = "{0} required")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
    public string Name { get; set; }

    [Required(ErrorMessage = "{0} required")]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "{0} required")]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "{0} required")]
    [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
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
