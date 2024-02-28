using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class SpecialHolidayViewModel
    {
        public int Id { get; set; }
        public HowToGiveEnum? HowToGive { get; set; }
        
        public DateTime? GiveDay { get; set; }
    }
    
   
}
