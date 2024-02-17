using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entity;

namespace WebApplication1.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "請填寫暱稱")]
        public string Name { get; set; } = null!;

        public string DepartmentName { get; set; } = null!;
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; } = null!;
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; } = null!;

        public bool Stop { get; set; }

    }
}
