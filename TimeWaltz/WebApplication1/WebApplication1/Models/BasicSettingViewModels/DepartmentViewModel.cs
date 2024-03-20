using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.BasicSettingViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "部門名稱")]

        public string DepartmentName { get; set; }

        [Display(Name = "部門主管")]
        public int? EmployeesId { get; set; }

        [NotMapped]
        public string? EmployeeName { get; set; }
        public string? QueryDepartment { get; set; }
    }
    public class DepartmentCreateShowDto
    {
        public List<SelectListItem>? EmployeeNameSelectList { get; set; }
    }
    public class DepartmentCreateDto
    {
        public string DepartmentName { get; set; }
        public int EmployeesId { get; set; }
    }
    public class DepartmentCreateViewModel
    {
        [Required(ErrorMessage = "請填部門名稱")]
        [Display(Name = "部門名稱")]
        public string DepartmentName { get; set; }

        public List<SelectListItem>? EmployeeNameSelectList { get; set; }

        [Display(Name = "部門主管")]
        public int? EmployeesId { get; set; }


    }

    public class DepartmentEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "部門名稱")]
        public string DepartmentName { get; set; }

        public List<SelectListItem>? EmployeeNameSelectList { get; set; }

        [Display(Name = "部門主管")]
        public int? EmployeesId { get; set; }



    }
}
