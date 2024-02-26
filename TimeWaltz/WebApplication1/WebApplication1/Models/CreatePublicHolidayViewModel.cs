using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CreatePublicHolidayViewModel
    {
        [Required]
        public string HolidayName { get; set; }
        
        public DateTime Date { get; set; }

    }
}
