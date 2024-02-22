using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class ClockViewModel
    {
        public int EmployeesId { get; set; }

        [Display(Name = "上班時間")]
        public DateTime Date { get; set; }

        public ClockStatusEnum Status { get; set; }

        [Display(Name = "緯度")]
        public decimal Latitude { get; set; }

        [Display(Name = "經度")]
        public decimal Longitude { get; set; }
    }
}
