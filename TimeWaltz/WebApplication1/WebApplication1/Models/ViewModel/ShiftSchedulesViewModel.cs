
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModel
{
    public class ShiftSchedulesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ShiftName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double BreakTimeHours { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }
    public class CreateShiftSchedulesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double BreakTimeHours { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }
    public class EditShiftSchedulesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string ShiftName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double BreakTimeHours { get; set; }
        public int MaxAdditionalClockIn { get; set; }

    }

}
