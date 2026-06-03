using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyber_Protect.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Employees_EmployeeID",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_EmployeeID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Incidents");

            migrationBuilder.AddColumn<int>(
                name: "IncidentID",
                table: "Threats",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Incidents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<string>(
                name: "AffectedSystems",
                table: "Incidents",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assign",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncidentID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Threats_IncidentID",
                table: "Threats",
                column: "IncidentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IncidentID",
                table: "Employees",
                column: "IncidentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Incidents_IncidentID",
                table: "Employees",
                column: "IncidentID",
                principalTable: "Incidents",
                principalColumn: "IncidentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Threats_Incidents_IncidentID",
                table: "Threats",
                column: "IncidentID",
                principalTable: "Incidents",
                principalColumn: "IncidentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Incidents_IncidentID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Threats_Incidents_IncidentID",
                table: "Threats");

            migrationBuilder.DropIndex(
                name: "IX_Threats_IncidentID",
                table: "Threats");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IncidentID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IncidentID",
                table: "Threats");

            migrationBuilder.DropColumn(
                name: "AffectedSystems",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "Assign",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "IncidentID",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Incidents",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_EmployeeID",
                table: "Incidents",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Employees_EmployeeID",
                table: "Incidents",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
