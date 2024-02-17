
namespace WebApplication1.Models
{
    public class ShiftSchedulesViewModel
    {
        public DateTime Date { get;  set; }
        public DayOfWeek DayOfWeek { get;  set; }
        public TimeSpan StartTime { get;  set; }
        public TimeSpan EndTime { get; set; }
        public double BreakTime { get; set; }
    }
}
