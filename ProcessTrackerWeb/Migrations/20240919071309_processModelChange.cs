using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProcessTrackerWeb.Migrations
{
    /// <inheritdoc />
    public partial class processModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RunDate",
                table: "Processes");

            migrationBuilder.AddColumn<string>(
                name: "RunHour",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RunHour",
                table: "Processes");

            migrationBuilder.AddColumn<DateTime>(
                name: "RunDate",
                table: "Processes",
                type: "datetime2",
                nullable: true);
        }
    }
}
