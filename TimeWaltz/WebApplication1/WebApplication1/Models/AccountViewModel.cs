using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        public string Account { get; set; }
        
        public int? EmployeesID { get; set; } = null!;
        
        public int? DepartmentID { get; set; } = null!;

        public bool Stop { get; set; }

    }
}
