using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Cook");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 20, 4, 33, 202, DateTimeKind.Local).AddTicks(2968));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Cook",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cook",
                keyColumn: "CookID",
                keyValue: 1,
                column: "SSN",
                value: "010100-4201");

            migrationBuilder.UpdateData(
                table: "Cook",
                keyColumn: "CookID",
                keyValue: 2,
                column: "SSN",
                value: "020200-1234");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 19, 59, 52, 325, DateTimeKind.Local).AddTicks(3794));
        }
    }
}
