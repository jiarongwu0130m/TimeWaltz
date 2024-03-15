using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models.SettingViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Display(Name = "姓名")]
        public List<SelectListItem>? EmployeesNameSelectList { get; set; } 

        [Display(Name = "單位")]
        public List<SelectListItem>? DepartmentNameSelectList { get; set; }

        public int? EmployeesName { get; set; }

        public int? DepartmentName { get; set; } 

        public bool Stop { get; set; }

    }

    public class UserCreateViewModel
    {

        [Display(Name = "帳號")]
        public string Account { get; set; }
        
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        public List<SelectListItem>? EmployeesNameSelectList { get; set; }

        [Display(Name = "單位")]
        public List<SelectListItem>? DepartmentNameSelectList { get; set; } 

        public int? EmployeesName { get; set; }

        public int? DepartmentName { get; set; } 

        public bool Stop { get; set; }

    }
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        public List<SelectListItem>? EmployeesNameSelectList { get; set; }

        [Display(Name = "單位")]
        public List<SelectListItem>? DepartmentNameSelectList { get; set; }

        [Display(Name = "密碼")]
        public string? Password { get; set; }

        public int? EmployeesName { get; set; }

        public int? DepartmentName { get; set; }

        [Display(Name = "停用/啟用")]
        public List<SelectListItem>? StopSelectList { get; set; }
        public bool Stop { get; set; }
        

    }
}
