using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class PersonalDataViewModel
    {
        public int Id { get; set; }

        public int ? ShiftScheduleId { get; set; }
        public string? ShiftsName { get; set; }
        public List<SelectListItem>? ShiftNameSelectItems { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public List<SelectListItem>? DepartmentNameSelectItem { get; set; }

        public string? QueryDepartmentName { get; set; }

        public string? Name { get; set; } 
        public string? QueryName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime HireDate { get; set; }

        public string? Email { get; set; }

        public GenderEnum Gender { get; set; }

        public string? EmployeesNo { get; set; } 
        public string? QueryEmployeesNo { get; set; }
    }
    public class CreatePersonalDataViewModel
    {
        public int? ShiftScheduleId { get; set; }
        public string? ShiftName { get; set; }
        public List<SelectListItem>? ShiftNameSelectItems { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public List<SelectListItem>? DepartmentNameSelectItem { get; set; }

        public string? Name { get; set; }

        public DateTime HireDate { get; set; }

        public string? Email { get; set; }

        public GenderEnum Gender { get; set; }
        
        public List<SelectListItem>? GenderSelectItems { get; set; }

        public string? EmployeesNo { get; set; }
    }
    public class EditPersonalDataViewModel
    {
        
        public int Id { get; set; }
        public int? ShiftScheduleId { get; set; }
        public string? ShiftName { get; set; }
        public List<SelectListItem>? ShiftNameSelectItems { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public List<SelectListItem>? DepartmentNameSelectItem { get; set; }

        public string? Name { get; set; }


        public string? Email { get; set; }


    }
}
