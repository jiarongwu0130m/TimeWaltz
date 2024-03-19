using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entity;

public partial class Employee
{
    public int Id { get; set; }

    public int? ShiftScheduleId { get; set; }

    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public string? Email { get; set; }

    public GenderEnum Gender { get; set; }

    public string EmployeesNo { get; set; } = null!;
    [DefaultValue(false)]
    public bool IsDelete { get; set; }
    [NotMapped]
    public string? ShiftsName { get; set; }
    [NotMapped]
    public string? DepartmentName { get; set; }
    [NotMapped]
    public string? AgentEmployeeName { get; set; }
    public virtual ICollection<AdditionalClockIn> AdditionalClockInApprovalEmployees { get; set; } = new List<AdditionalClockIn>();

    public virtual ICollection<AdditionalClockIn> AdditionalClockInEmployees { get; set; } = new List<AdditionalClockIn>();

    public virtual ICollection<AgentEmployee> AgentEmployeeAgentEmployees { get; set; } = new List<AgentEmployee>();

    public virtual ICollection<AgentEmployee> AgentEmployeeEmployees { get; set; } = new List<AgentEmployee>();

    public virtual ICollection<Billboard> Billboards { get; set; } = new List<Billboard>();

    public virtual ICollection<Clock> Clocks { get; set; } = new List<Clock>();

    public virtual ICollection<LeaveRequest> LeaveRequestAgentEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestApprovalEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<OvertimeApplication> OvertimeApplicationApprovalEmployees { get; set; } = new List<OvertimeApplication>();

    public virtual ICollection<OvertimeApplication> OvertimeApplicationEmployees { get; set; } = new List<OvertimeApplication>();

    public virtual ShiftSchedule? ShiftSchedule { get; set; }

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual User? User { get; set; }
}
