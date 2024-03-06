using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.BasicSettingViewModels
{
    public class PersonalDataDto
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }
        public string Name { get; set; } 

        public DateTime HireDate { get; set; }

        public string? Email { get; set; }

        public GenderEnum Gender { get; set; }

        public string EmployeesNo { get; set; } 
  
        public string? ShiftsName { get; set; }

    }

    public class GenderDropDownDto
    {
        public List<SelectListItem> GenderSelectItems { get; set; }
    }
}
