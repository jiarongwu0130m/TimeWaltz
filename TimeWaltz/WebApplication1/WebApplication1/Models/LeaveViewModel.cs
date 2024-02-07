
using WebApplication1.Models.Enums;

namespace WebApplication1.Models
{
    public class LeaveViewModel
    {
        //public int RequestEmployee { get; internal set; }
        public DateTime StartTime { get; internal set; }
        public DateTime EndTime { get; internal set; }
        public string VacationName { get; internal set; }
        public string ApprovalStatus { get; internal set; }
        public int LeaveHour { get; internal set; }
    }
}
