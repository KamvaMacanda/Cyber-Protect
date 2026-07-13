using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyber_Protect.Migrations
{
    /// <inheritdoc />
    public partial class FixModelLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IncidentID",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "AddNotes",
                table: "Incidents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThreatID",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_EmployeeID",
                table: "Incidents",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ThreatID",
                table: "Incidents",
                column: "ThreatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Employees_EmployeeID",
                table: "Incidents",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_Threats_ThreatID",
                table: "Incidents",
                column: "ThreatID",
                principalTable: "Threats",
                principalColumn: "ThreatID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Employees_EmployeeID",
                table: "Incidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Incidents_Threats_ThreatID",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_EmployeeID",
                table: "Incidents");

            migrationBuilder.DropIndex(
                name: "IX_Incidents_ThreatID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "AddNotes",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Incidents");

            migrationBuilder.DropColumn(
                name: "ThreatID",
                table: "Incidents");

            migrationBuilder.AddColumn<int>(
                name: "IncidentID",
                table: "Threats",
                type: "int",
                nullable: true);

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
    }
}
