using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 20, 51, 10, 265, DateTimeKind.Local).AddTicks(150));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 20, 4, 33, 202, DateTimeKind.Local).AddTicks(2968));
        }
    }
}
