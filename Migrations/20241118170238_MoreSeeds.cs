using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class MoreSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerID", "Address", "Name", "Payment_Info" },
                values: new object[] { 2, "Ny Munkegade 118, 8200 Aarhus N", "Dijkstra", "MobilePay" });

            migrationBuilder.InsertData(
                table: "Cyclist",
                columns: new[] { "CyclistID", "BikeType", "Hourly_rate", "Name" },
                values: new object[] { 2, "Mountain Bike", 80.00m, "Moon" });

            migrationBuilder.InsertData(
                table: "Dish",
                columns: new[] { "DishID", "AvailableFrom", "AvailableTo", "CookID", "Name", "Price" },
                values: new object[] { 4, new DateTime(2024, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, "Burger", 50.00m });

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 18, 18, 2, 38, 79, DateTimeKind.Local).AddTicks(8065));

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingID", "CookID", "CustomerID", "CyclistID", "DeliveryScore", "FoodScore" },
                values: new object[] { 3, 2, 1, 1, 3, 3 });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderID", "CustomerID", "OrderDate" },
                values: new object[] { 2, 2, new DateTime(2024, 11, 18, 18, 2, 38, 79, DateTimeKind.Local).AddTicks(8121) });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingID", "CookID", "CustomerID", "CyclistID", "DeliveryScore", "FoodScore" },
                values: new object[] { 2, 1, 1, 2, 4, 4 });

            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "OrderDetailID", "DishID", "OrderID", "Quantity" },
                values: new object[] { 3, 3, 2, 1 });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "TripID", "CyclistID", "OrderID" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "TripStops",
                columns: new[] { "TripStopsID", "ActionType", "StopAddress", "StopTime", "TripID" },
                values: new object[,]
                {
                    { 3, "Picked Up", "Ny Munkegade 118, 8200 Aarhus N", new DateTime(2024, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, "Delivered", "Finsensgade 1493, 8000 Aarhus", new DateTime(2024, 9, 15, 16, 16, 0, 0, DateTimeKind.Unspecified), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dish",
                keyColumn: "DishID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TripStops",
                keyColumn: "TripStopsID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TripStops",
                keyColumn: "TripStopsID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trip",
                keyColumn: "TripID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cyclist",
                keyColumn: "CyclistID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerID",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 18, 17, 27, 41, 857, DateTimeKind.Local).AddTicks(4979));
        }
    }
}
