
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.ViewModel
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        public int RequestEmployee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string VacationName { get; set; }
        public string ApprovalStatus { get; set; }
        public int LeaveHour { get; set; }

    }
    public class LeaveCreateViewModel
    {
        public int RequestEmployee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string VacationType { get; set; }

        public int LeaveHour { get; set; }
        public string Reason { get; set; }
        public string? FileRoute { get; set; }
        public List<SelectListItem>? AgentEmployeeSelectItems { get; set; }
        public int AgentEmployeeId { get; set; }
        public List<SelectListItem>? VacationTypeSelectItems { get; set; }

    }

}
