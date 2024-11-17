using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cook",
                columns: new[] { "CookID", "Address", "Name", "Phone", "SSN" },
                values: new object[,]
                {
                    { 1, "Finlandsgade 17, 8200 Aarhus N", "Noah", "+45 71555080", "010100-4201" },
                    { 2, "Ny Munkegade 118, 8200 Aarhus N", "Helle", "+45 71555081", "020200-1234" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerID", "Address", "Name", "Payment_Info" },
                values: new object[] { 1, "Finsensgade 1493, 8000 Aarhus", "Knuth", "Card" });

            migrationBuilder.InsertData(
                table: "Cyclist",
                columns: new[] { "CyclistID", "BikeType", "Hourly_rate", "Name" },
                values: new object[] { 1, "Electric Bike", 100.00m, "Star" });

            migrationBuilder.InsertData(
                table: "Dish",
                columns: new[] { "DishID", "AvailableFrom", "AvailableTo", "CookID", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 15, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), 1, "Pasta", 30.00m },
                    { 2, new DateTime(2024, 9, 15, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), 1, "Romkugle", 3.00m },
                    { 3, new DateTime(2024, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, "Lemonade", 15.00m }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderID", "CustomerID", "OrderDate" },
                values: new object[] { 1, 1, new DateTime(2024, 10, 27, 16, 12, 54, 519, DateTimeKind.Local).AddTicks(2292) });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingID", "CookID", "CustomerID", "CyclistID", "DeliveryScore", "FoodScore" },
                values: new object[] { 1, 1, 1, 1, 5, 5 });

            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "OrderDetailID", "DishID", "OrderID", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Trip",
                columns: new[] { "TripID", "CyclistID", "OrderID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "TripStops",
                columns: new[] { "TripStopsID", "ActionType", "StopAddress", "StopTime", "TripID" },
                values: new object[,]
                {
                    { 1, "Picked Up", "Finlandsgade 17, 8200 Aarhus N", new DateTime(2024, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Delivered", "Finsensgade 1493, 8000 Aarhus", new DateTime(2024, 9, 15, 12, 16, 0, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dish",
                keyColumn: "DishID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "RatingID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TripStops",
                keyColumn: "TripStopsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TripStops",
                keyColumn: "TripStopsID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cook",
                keyColumn: "CookID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dish",
                keyColumn: "DishID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dish",
                keyColumn: "DishID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trip",
                keyColumn: "TripID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cook",
                keyColumn: "CookID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cyclist",
                keyColumn: "CyclistID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "CustomerID",
                keyValue: 1);
        }
    }
}
