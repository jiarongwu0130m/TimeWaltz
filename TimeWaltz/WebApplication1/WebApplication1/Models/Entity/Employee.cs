using System;
using System.Collections.Generic;

namespace WebApplication1.Models.Entity;

public partial class Employee
{
    public int Id { get; set; }

    public int ShiftScheduleId { get; set; }

    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public string? Email { get; set; }

    public bool Gender { get; set; }

    public string EmployeesNo { get; set; } = null!;

    public virtual ICollection<AdditionalClockIn> AdditionalClockInApprovalEmployees { get; set; } = new List<AdditionalClockIn>();

    public virtual ICollection<AdditionalClockIn> AdditionalClockInEmployees { get; set; } = new List<AdditionalClockIn>();

    public virtual ICollection<Billboard> Billboards { get; set; } = new List<Billboard>();

    public virtual ICollection<Clock> Clocks { get; set; } = new List<Clock>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<LeaveRequest> LeaveRequestAgentEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestApprovalEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<OvertiomeApplication> OvertiomeApplications { get; set; } = new List<OvertiomeApplication>();

    public virtual ShiftSchedule ShiftSchedule { get; set; } = null!;

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
