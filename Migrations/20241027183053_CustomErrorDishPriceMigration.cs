using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class CustomErrorDishPriceMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 19, 30, 52, 823, DateTimeKind.Local).AddTicks(3928));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 27, 16, 12, 54, 519, DateTimeKind.Local).AddTicks(2292));
        }
    }
}
