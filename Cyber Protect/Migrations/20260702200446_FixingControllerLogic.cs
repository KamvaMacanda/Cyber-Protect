using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cyber_Protect.Migrations
{
    /// <inheritdoc />
    public partial class FixingControllerLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Employees",
                newName: "Analyst");

            migrationBuilder.AlterColumn<string>(
                name: "level",
                table: "Threats",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLogged",
                table: "Threats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LoggedBy",
                table: "Threats",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLogged",
                table: "Threats");

            migrationBuilder.DropColumn(
                name: "LoggedBy",
                table: "Threats");

            migrationBuilder.RenameColumn(
                name: "Analyst",
                table: "Employees",
                newName: "FullName");

            migrationBuilder.AlterColumn<int>(
                name: "level",
                table: "Threats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
