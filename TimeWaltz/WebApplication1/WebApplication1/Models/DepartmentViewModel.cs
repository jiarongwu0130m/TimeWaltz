using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Display(Name ="部門名稱")]
        
        public string DepartmentName { get; set; }

        [Display(Name = "部門主管")]
        public int EmployeesId { get; set; }

        public string? QueryDepartment { get; set; }


        

    }
    public class DepartmentCreateViewModel
    {
        [Required(ErrorMessage = "請填部門名稱")]
        [Display(Name = "部門名稱")]
        public string DepartmentName { get; set; }

        [Display(Name = "部門主管")]
        public int EmployeesId { get; set; }

    }

    public class DepartmentEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "部門名稱")]
        public string DepartmentName { get; set; }

        [Display(Name = "部門主管")]
        public int EmployeesId { get; set; }

        //public int? DepartmentId { get; set; }

    }
}
