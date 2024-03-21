using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models.BasicSettingViewModels
{
    public class PersonalDataDto
    {
        public int Id { get; set; }

        public string DepartmentName { get; set; }
        public string Name { get; set; } 

        public DateTime HireDate { get; set; }

        public string? Email { get; set; }

        public string Gender { get; set; }

        public string EmployeesNo { get; set; } 
  
        public string? ShiftsName { get; set; }

    }
    public class PersonalDataEditShowDto
    {
        public int Id { get; set; }
        public int? ShiftScheduleId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string EmployeesNo { get; set; }
        public int Gender { get; set; }
    }
    public class PersonalDataEditDto
    {
        public int Id { get; set; }
        public int? ShiftScheduleId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string EmployeesNo { get; set; }


    }
    public class PersonalDataEditModel
    {
        public int Id { get; set; }
        public int? ShiftScheduleId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string EmployeesNo { get; set; }
        public DateTime HireDate { get; set; }
    }
    public class DepAndShiftDropDownDto
    {
        public List<SelectListItem> DepartmentNameSelectItem { get; set; }
        public List<SelectListItem> ShiftNameSelectItems { get; set; }
    }
    public class GenderDropDownDto
    {
        public List<SelectListItem> GenderSelectItems { get; set; }
    }

    public class PersonalDataCreateDto
    {

        public int DepartmentId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime HireDate { get; set; }

        public string? Email { get; set; }

        public int Gender { get; set; }

        public string EmployeesNo { get; set; }

        public int? ShiftScheduleId { get; set; }

    }
    public class EmpIdNameGet
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
