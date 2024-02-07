using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Entity;

public partial class TimeWaltzContext : DbContext
{
    public TimeWaltzContext()
    {
    }

    public TimeWaltzContext(DbContextOptions<TimeWaltzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<AccessRoleBind> AccessRoleBinds { get; set; }

    public virtual DbSet<AdditionalClockIn> AdditionalClockIns { get; set; }

    public virtual DbSet<AgentEmployee> AgentEmployees { get; set; }

    public virtual DbSet<AllLeaveDay> AllLeaveDays { get; set; }

    public virtual DbSet<Approval> Approvals { get; set; }

    public virtual DbSet<Billboard> Billboards { get; set; }

    public virtual DbSet<Clock> Clocks { get; set; }

    public virtual DbSet<CompLeave> CompLeaves { get; set; }

    public virtual DbSet<CompLeaveUseRecord> CompLeaveUseRecords { get; set; }

    public virtual DbSet<CompanyLocation> CompanyLocations { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Flextime> Flextimes { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<OvertimeApplication> OvertimeApplications { get; set; }

    public virtual DbSet<PublicHoliday> PublicHolidays { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftSchedule> ShiftSchedules { get; set; }

    public virtual DbSet<SpacialVacation> SpacialVacations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOfDepartment> UserOfDepartments { get; set; }

    public virtual DbSet<UserRoleBind> UserRoleBinds { get; set; }

    public virtual DbSet<VacationDetail> VacationDetails { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.ToTable("Access");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.Controller).HasMaxLength(50);
            entity.Property(e => e.ManuName).HasMaxLength(50);
        });

        modelBuilder.Entity<AccessRoleBind>(entity =>
        {
            entity.ToTable("AccessRoleBind");

            entity.HasIndex(e => e.AccessId, "IX_AccessRoleBind_AccessID");

            entity.HasIndex(e => e.RoleId, "IX_AccessRoleBind_RoleID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccessId).HasColumnName("AccessID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Access).WithMany(p => p.AccessRoleBinds)
                .HasForeignKey(d => d.AccessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessRoleBind_Access");

            entity.HasOne(d => d.Role).WithMany(p => p.AccessRoleBinds)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccessRoleBind_Role");
        });

        modelBuilder.Entity<AdditionalClockIn>(entity =>
        {
            entity.ToTable("AdditionalClockIn");

            entity.HasIndex(e => e.ApprovalEmployeeId, "IX_AdditionalClockIn_ApprovalEmployeeID");

            entity.HasIndex(e => e.EmployeesId, "IX_AdditionalClockIn_EmployeesID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdditionalTime).HasColumnType("datetime");
            entity.Property(e => e.ApprovalEmployeeId).HasColumnName("ApprovalEmployeeID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.ApprovalEmployee).WithMany(p => p.AdditionalClockInApprovalEmployees)
                .HasForeignKey(d => d.ApprovalEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalClockIn_Employees3");

            entity.HasOne(d => d.Employees).WithMany(p => p.AdditionalClockInEmployees)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalClockIn_Employees2");
        });

        modelBuilder.Entity<AgentEmployee>(entity =>
        {
            entity.HasIndex(e => e.AgentEmployeesId, "IX_AgentEmployees_AgentEmployeesID");

            entity.HasIndex(e => e.EmployeesId, "IX_AgentEmployees_EmployeesID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgentEmployeesId).HasColumnName("AgentEmployeesID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");

            entity.HasOne(d => d.AgentEmployees).WithMany(p => p.AgentEmployeeAgentEmployees)
                .HasForeignKey(d => d.AgentEmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgentEmployees_Employees1");

            entity.HasOne(d => d.Employees).WithMany(p => p.AgentEmployeeEmployees)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgentEmployees_Employees");
        });

        modelBuilder.Entity<AllLeaveDay>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.VacationDetailsId).HasColumnName("VacationDetailsID");
            entity.Property(e => e.VacationHoursRemain).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Approval>(entity =>
        {
            entity.ToTable("Approval");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.TableId).HasColumnName("TableID");
        });

        modelBuilder.Entity<Billboard>(entity =>
        {
            entity.ToTable("Billboard");

            entity.HasIndex(e => e.EmployeesId, "IX_Billboard_EmployeesID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(20);

            entity.HasOne(d => d.Employees).WithMany(p => p.Billboards)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Billboard_Employees");
        });

        modelBuilder.Entity<Clock>(entity =>
        {
            entity.ToTable("Clock");

            entity.HasIndex(e => e.EmployeesId, "IX_Clock_EmployeesID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Employees).WithMany(p => p.Clocks)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clock_Employees");
        });

        modelBuilder.Entity<CompLeave>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CompRequest");

            entity.ToTable("CompLeave");

            entity.HasIndex(e => e.OvertimeId, "IX_CompRequest_OvertimeID");

            entity.HasIndex(e => e.VacationDetailsId, "IX_CompRequest_VacationDetailsID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.OvertimeId).HasColumnName("OvertimeID");
            entity.Property(e => e.VacationDetailsId).HasColumnName("VacationDetailsID");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.CompLeave)
                .HasForeignKey<CompLeave>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompLeave_OvertimeApplication");
        });

        modelBuilder.Entity<CompLeaveUseRecord>(entity =>
        {
            entity.ToTable("CompLeaveUseRecord");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompLeaveId).HasColumnName("CompLeaveID");
            entity.Property(e => e.LeaveRequestId).HasColumnName("LeaveRequestID");

            entity.HasOne(d => d.CompLeave).WithMany(p => p.CompLeaveUseRecords)
                .HasForeignKey(d => d.CompLeaveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompLeaveUseRecord_CompLeave");

            entity.HasOne(d => d.LeaveRequest).WithMany(p => p.CompLeaveUseRecords)
                .HasForeignKey(d => d.LeaveRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompLeaveUseRecord_LeaveRequest");
        });

        modelBuilder.Entity<CompanyLocation>(entity =>
        {
            entity.ToTable("CompanyLocation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentId, "IX_Department_DepartmentID");

            entity.HasIndex(e => e.EmployeesId, "IX_Department_EmployeesID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.ShiftScheduleId, "IX_Employees_ShiftScheduleID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmployeesNo).HasMaxLength(50);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShiftScheduleId).HasColumnName("ShiftScheduleID");

            entity.HasOne(d => d.ShiftSchedule).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ShiftScheduleId)
                .HasConstraintName("FK_Employees_ShiftSchedule");
        });

        modelBuilder.Entity<Flextime>(entity =>
        {
            entity.ToTable("Flextime");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.ToTable("LeaveRequest");

            entity.HasIndex(e => e.AgentEmployeeId, "IX_LeaveRequest_AgentEmployeeID");

            entity.HasIndex(e => e.ApprovalEmployeeId, "IX_LeaveRequest_ApprovalEmployeeID");

            entity.HasIndex(e => e.EmployeesId, "IX_LeaveRequest_EmployeesID");

            entity.HasIndex(e => e.VacationDetailsId, "IX_LeaveRequest_VacationDetailsID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AgentEmployeeId).HasColumnName("AgentEmployeeID");
            entity.Property(e => e.ApprovalEmployeeId).HasColumnName("ApprovalEmployeeID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FileRoute).IsUnicode(false);
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.VacationDetailsId).HasColumnName("VacationDetailsID");

            entity.HasOne(d => d.AgentEmployee).WithMany(p => p.LeaveRequestAgentEmployees)
                .HasForeignKey(d => d.AgentEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employees1");

            entity.HasOne(d => d.ApprovalEmployee).WithMany(p => p.LeaveRequestApprovalEmployees)
                .HasForeignKey(d => d.ApprovalEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employees2");

            entity.HasOne(d => d.Employees).WithMany(p => p.LeaveRequestEmployees)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employees");

            entity.HasOne(d => d.VacationDetails).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.VacationDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_VacationDetails");
        });

        modelBuilder.Entity<OvertimeApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OvertiomeApplication");

            entity.ToTable("OvertimeApplication");

            entity.HasIndex(e => e.EmployeesId, "IX_OvertiomeApplication_EmployeesID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApprovalEmployeeId).HasColumnName("ApprovalEmployeeID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.ApprovalEmployee).WithMany(p => p.OvertimeApplicationApprovalEmployees)
                .HasForeignKey(d => d.ApprovalEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OvertimeApplication_Employees");

            entity.HasOne(d => d.Employees).WithMany(p => p.OvertimeApplicationEmployees)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OvertiomeApplication_Employees");
        });

        modelBuilder.Entity<PublicHoliday>(entity =>
        {
            entity.ToTable("PublicHoliday");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.HolidayName).HasMaxLength(50);
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.ToTable("RequestStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TableId).HasColumnName("TableID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift");

            entity.HasIndex(e => e.EmployeesId, "IX_Shift_EmployeesID");

            entity.HasIndex(e => e.ShiftScheduleId, "IX_Shift_ShiftScheduleID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.ShiftScheduleId).HasColumnName("ShiftScheduleID");
            entity.Property(e => e.ShiftsDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employees).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.EmployeesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_Employees");

            entity.HasOne(d => d.ShiftSchedule).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.ShiftScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_ShiftSchedule");
        });

        modelBuilder.Entity<ShiftSchedule>(entity =>
        {
            entity.ToTable("ShiftSchedule");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.ShiftsName).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<SpacialVacation>(entity =>
        {
            entity.ToTable("SpacialVacation");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.SpacialVacationName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.DepartmentId, "IX_User_DepartmentID");

            entity.HasIndex(e => e.EmployeesId, "IX_User_EmployeesID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Account).HasMaxLength(50);
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmployeesId).HasColumnName("EmployeesID");
            entity.Property(e => e.PasswordDate).HasColumnType("datetime");
            entity.Property(e => e.Salt).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Department");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Employees1");

            entity.HasOne(d => d.Id1).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserOfDepartment");
        });

        modelBuilder.Entity<UserOfDepartment>(entity =>
        {
            entity.ToTable("UserOfDepartment");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<UserRoleBind>(entity =>
        {
            entity.ToTable("UserRoleBind");

            entity.HasIndex(e => e.RoleId, "IX_UserRoleBind_RoleID");

            entity.HasIndex(e => e.UserId, "IX_UserRoleBind_UserID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleBinds)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoleBind_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleBinds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoleBind_User");
        });

        modelBuilder.Entity<VacationDetail>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.VacationType).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
