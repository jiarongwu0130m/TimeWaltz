using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class TimeWaltzContext : DbContext
{
    public TimeWaltzContext(DbContextOptions<TimeWaltzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<AdditionalClockIn> AdditionalClockIns { get; set; }

    public virtual DbSet<Approval> Approvals { get; set; }

    public virtual DbSet<Billboard> Billboards { get; set; }

    public virtual DbSet<Clock> Clocks { get; set; }

    public virtual DbSet<CompLeave> CompLeaves { get; set; }

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

    public virtual DbSet<SpecialGrade> SpecialGrades { get; set; }

    public virtual DbSet<SpecialHoliday> SpecialHolidays { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOfAdmin> UserOfAdmins { get; set; }

    public virtual DbSet<VacationDetail> VacationDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasMany(d => d.Roles).WithMany(p => p.Accesses)
                .UsingEntity<Dictionary<string, object>>(
                    "AccessRoleBind",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AccessRoleBind_Role"),
                    l => l.HasOne<Access>().WithMany()
                        .HasForeignKey("AccessId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AccessRoleBind_Access"),
                    j =>
                    {
                        j.HasKey("AccessId", "RoleId");
                        j.ToTable("AccessRoleBind");
                        j.IndexerProperty<int>("AccessId").HasColumnName("AccessID");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                    });
        });

        modelBuilder.Entity<AdditionalClockIn>(entity =>
        {
            entity.HasOne(d => d.Employees).WithMany(p => p.AdditionalClockIns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalClockIn_Employees");
        });

        modelBuilder.Entity<Billboard>(entity =>
        {
            entity.HasOne(d => d.Employees).WithMany(p => p.Billboards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Billboard_Employees");
        });

        modelBuilder.Entity<Clock>(entity =>
        {
            entity.HasOne(d => d.Employees).WithMany(p => p.Clocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clock_Employees");
        });

        modelBuilder.Entity<CompLeave>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CompRequest");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Overtime).WithMany(p => p.CompLeaves)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompLeave_OvertimeApplication");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Department");

            entity.HasOne(d => d.ShiftSchedule).WithMany(p => p.Employees).HasConstraintName("FK_Employees_ShiftSchedule");

            entity.HasMany(d => d.AgentEmployees).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "AgentEmployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("AgentEmployeesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AgentEmployees_Employees1"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AgentEmployees_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeesId", "AgentEmployeesId");
                        j.ToTable("AgentEmployees");
                        j.IndexerProperty<int>("EmployeesId").HasColumnName("EmployeesID");
                        j.IndexerProperty<int>("AgentEmployeesId").HasColumnName("AgentEmployeesID");
                    });

            entity.HasMany(d => d.Employees).WithMany(p => p.AgentEmployees)
                .UsingEntity<Dictionary<string, object>>(
                    "AgentEmployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AgentEmployees_Employees"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("AgentEmployeesId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AgentEmployees_Employees1"),
                    j =>
                    {
                        j.HasKey("EmployeesId", "AgentEmployeesId");
                        j.ToTable("AgentEmployees");
                        j.IndexerProperty<int>("EmployeesId").HasColumnName("EmployeesID");
                        j.IndexerProperty<int>("AgentEmployeesId").HasColumnName("AgentEmployeesID");
                    });
        });

        modelBuilder.Entity<Flextime>(entity =>
        {
            entity.Property(e => e.MoveUp).HasDefaultValueSql("(CONVERT([bit],(0)))");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasOne(d => d.AgentEmployee).WithMany(p => p.LeaveRequestAgentEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employees1");

            entity.HasOne(d => d.ApprovalEmployee).WithMany(p => p.LeaveRequestApprovalEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employees2");

            entity.HasOne(d => d.Employees).WithMany(p => p.LeaveRequestEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_Employees");

            entity.HasOne(d => d.VacationDetails).WithMany(p => p.LeaveRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveRequest_VacationDetails");
        });

        modelBuilder.Entity<OvertimeApplication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OvertiomeApplication");

            entity.HasOne(d => d.Employees).WithMany(p => p.OvertimeApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OvertimeApplication_Employees");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasOne(d => d.Employees).WithMany(p => p.Shifts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_Employees");

            entity.HasOne(d => d.ShiftSchedule).WithMany(p => p.Shifts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_ShiftSchedule");
        });

        modelBuilder.Entity<SpecialGrade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GradeTable");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Salt).HasDefaultValueSql("(N'')");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Employees");

            entity.HasOne(d => d.UserOfAdmin).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserOfAdmin");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<UserOfAdmin>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}