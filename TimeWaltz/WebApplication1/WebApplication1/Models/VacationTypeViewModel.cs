using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class VacationTypeViewModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="請填假別名稱")]
        public string VacationType { get; set; }
        
        public GenderEnum? Gender { get; set; }
        public List<SelectListItem> GenderSelectItems { get; set; } 
  
        public int NumberOfDays { get; set; }
        
        public List<SelectListItem> CycleSelectItems { get; set; }
        public CycleEnum Cycle { get; set; }

 
        public int MinVacationHours { get; set; }
    }
}
