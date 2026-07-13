using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyber_Protect.Migrations
{
    /// <inheritdoc />
    public partial class DeleteModelData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoggedBy",
                table: "Threats");

            migrationBuilder.DropColumn(
                name: "Assign",
                table: "Incidents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoggedBy",
                table: "Threats",
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
        }
    }
}
