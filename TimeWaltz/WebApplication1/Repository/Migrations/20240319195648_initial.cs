using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManuName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccessRole",
                columns: table => new
                {
                    AccessId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRole", x => new { x.AccessId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableType = table.Column<int>(type: "int", nullable: false),
                    TableID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEmployee",
                columns: table => new
                {
                    AgentEmployeesId = table.Column<int>(type: "int", nullable: false),
                    EmployeesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEmployee", x => new { x.AgentEmployeesId, x.EmployeesId });
                });

            migrationBuilder.CreateTable(
                name: "Flextime",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlexibleTime = table.Column<int>(type: "int", nullable: false),
                    MoveUp = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(0)))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flextime", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PublicHoliday",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicHoliday", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableType = table.Column<int>(type: "int", nullable: false),
                    TableID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShiftSchedule",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ShiftsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BreakTime = table.Column<int>(type: "int", nullable: false),
                    MaxAdditionalClockIn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSchedule", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SpecialGrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceLength = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HowToGive = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialHoliday", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialVacation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialVacationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialVacation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VacationDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    Cycle = table.Column<int>(type: "int", nullable: false),
                    MinVacationHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccessRoleBind",
                columns: table => new
                {
                    AccessID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRoleBind", x => new { x.AccessID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_AccessRoleBind_Access",
                        column: x => x.AccessID,
                        principalTable: "Access",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccessRoleBind_Role",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Stop = table.Column<bool>(type: "bit", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "(N'')"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    ShiftScheduleID = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    EmployeesNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Department",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employees_ShiftSchedule",
                        column: x => x.ShiftScheduleID,
                        principalTable: "ShiftSchedule",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_Employees",
                        column: x => x.ID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserOfAdmin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOfAdmin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_UserOfAdmin",
                        column: x => x.ID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalClockIn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    AdditionalTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApprovalEmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalClockIn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdditionalClockIn_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AgentEmployees",
                columns: table => new
                {
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    AgentEmployeesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentEmployees", x => new { x.EmployeesID, x.AgentEmployeesID });
                    table.ForeignKey(
                        name: "FK_AgentEmployees_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AgentEmployees_Employees1",
                        column: x => x.AgentEmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Billboard",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    PathRoute = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billboard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Billboard_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Clock",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clock", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Clock_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequest",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    VacationDetailsID = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileRoute = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AgentEmployeeID = table.Column<int>(type: "int", nullable: false),
                    ApprovalEmployeeID = table.Column<int>(type: "int", nullable: false),
                    LeaveMinutes = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeaveRequest_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeaveRequest_Employees1",
                        column: x => x.AgentEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeaveRequest_Employees2",
                        column: x => x.ApprovalEmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeaveRequest_VacationDetails",
                        column: x => x.VacationDetailsID,
                        principalTable: "VacationDetails",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OvertimeApplication",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalEmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertiomeApplication", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OvertimeApplication_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftsDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmployeesID = table.Column<int>(type: "int", nullable: false),
                    ShiftScheduleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shift_Employees",
                        column: x => x.EmployeesID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Shift_ShiftSchedule",
                        column: x => x.ShiftScheduleID,
                        principalTable: "ShiftSchedule",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SpecialHolidayDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AvailableDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialHolidayDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialHolidayDays_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompLeave",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    OvertimeID = table.Column<int>(type: "int", nullable: false),
                    CompMin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompLeave_OvertimeApplication",
                        column: x => x.OvertimeID,
                        principalTable: "OvertimeApplication",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessRoleBind_RoleID",
                table: "AccessRoleBind",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalClockIn_EmployeesID",
                table: "AdditionalClockIn",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_AgentEmployees_AgentEmployeesID",
                table: "AgentEmployees",
                column: "AgentEmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_Billboard_EmployeesID",
                table: "Billboard",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_Clock_EmployeesID",
                table: "Clock",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_CompLeave_OvertimeID",
                table: "CompLeave",
                column: "OvertimeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftScheduleID",
                table: "Employees",
                column: "ShiftScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_AgentEmployeeID",
                table: "LeaveRequest",
                column: "AgentEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_ApprovalEmployeeID",
                table: "LeaveRequest",
                column: "ApprovalEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_EmployeesID",
                table: "LeaveRequest",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_VacationDetailsID",
                table: "LeaveRequest",
                column: "VacationDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeApplication_EmployeesID",
                table: "OvertimeApplication",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_EmployeesID",
                table: "Shift",
                column: "EmployeesID");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ShiftScheduleID",
                table: "Shift",
                column: "ShiftScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialHolidayDays_EmployeeId",
                table: "SpecialHolidayDays",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRole");

            migrationBuilder.DropTable(
                name: "AccessRoleBind");

            migrationBuilder.DropTable(
                name: "AdditionalClockIn");

            migrationBuilder.DropTable(
                name: "AgentEmployees");

            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropTable(
                name: "Billboard");

            migrationBuilder.DropTable(
                name: "Clock");

            migrationBuilder.DropTable(
                name: "CompLeave");

            migrationBuilder.DropTable(
                name: "EmployeeEmployee");

            migrationBuilder.DropTable(
                name: "Flextime");

            migrationBuilder.DropTable(
                name: "LeaveRequest");

            migrationBuilder.DropTable(
                name: "PublicHoliday");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "SpecialGrade");

            migrationBuilder.DropTable(
                name: "SpecialHoliday");

            migrationBuilder.DropTable(
                name: "SpecialHolidayDays");

            migrationBuilder.DropTable(
                name: "SpecialVacation");

            migrationBuilder.DropTable(
                name: "UserOfAdmin");

            migrationBuilder.DropTable(
                name: "Access");

            migrationBuilder.DropTable(
                name: "OvertimeApplication");

            migrationBuilder.DropTable(
                name: "VacationDetails");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "ShiftSchedule");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
