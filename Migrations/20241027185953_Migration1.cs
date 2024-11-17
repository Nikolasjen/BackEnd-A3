using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PassedCourse",
                table: "Cook",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cook",
                keyColumn: "CookID",
                keyValue: 1,
                column: "PassedCourse",
                value: true);

            migrationBuilder.UpdateData(
                table: "Cook",
                keyColumn: "CookID",
                keyValue: 2,
                column: "PassedCourse",
                value: true);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 19, 59, 52, 325, DateTimeKind.Local).AddTicks(3794));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassedCourse",
                table: "Cook");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 19, 30, 52, 823, DateTimeKind.Local).AddTicks(3928));
        }
    }
}
