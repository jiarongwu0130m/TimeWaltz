
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ShiftSchedulesViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get;  set; }
        public DayOfWeek DayOfWeek { get;  set; }                
        public DateTime StartTime { get;  set; } 
        public DateTime EndTime { get; set; }       
        public double BreakTime { get; set; }
        
    }
}
