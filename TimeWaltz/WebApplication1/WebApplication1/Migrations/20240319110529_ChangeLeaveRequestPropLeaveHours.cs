using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLeaveRequestPropLeaveHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveHours",
                table: "LeaveRequest");

            migrationBuilder.AddColumn<decimal>(
                name: "LeaveMinutes",
                table: "LeaveRequest",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveMinutes",
                table: "LeaveRequest");

            migrationBuilder.AddColumn<int>(
                name: "LeaveHours",
                table: "LeaveRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
