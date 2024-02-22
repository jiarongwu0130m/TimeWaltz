
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ShiftSchedulesViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get;  set; }
        public DayOfWeek DayOfWeek { get;  set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get;  set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        
        public double BreakTime { get; set; }
        
    }
}
