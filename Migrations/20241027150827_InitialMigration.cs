using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodAppG4.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cook",
                columns: table => new
                {
                    CookID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    SSN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cook__E51807016A4A9DE2", x => x.CookID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Payment_Info = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B8FF097CAC", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Cyclist",
                columns: table => new
                {
                    CyclistID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Hourly_rate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    BikeType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cyclist__A3CBF9E0134B5CEE", x => x.CyclistID);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    DishID = table.Column<int>(type: "int", nullable: false),
                    CookID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvailableTo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dish__18834F7058F04B29", x => x.DishID);
                    table.ForeignKey(
                        name: "FK__Dish__CookID__1AD3FDA4",
                        column: x => x.CookID,
                        principalTable: "Cook",
                        principalColumn: "CookID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__C3905BAFF98CAB4A", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Order__CustomerI__17F790F9",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false),
                    CookID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    CyclistID = table.Column<int>(type: "int", nullable: true),
                    DeliveryScore = table.Column<int>(type: "int", nullable: true),
                    FoodScore = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rating__FCCDF85C1B6CDE4B", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK__Rating__CookID__2B0A656D",
                        column: x => x.CookID,
                        principalTable: "Cook",
                        principalColumn: "CookID");
                    table.ForeignKey(
                        name: "FK__Rating__Customer__2BFE89A6",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Rating__CyclistI__2CF2ADDF",
                        column: x => x.CyclistID,
                        principalTable: "Cyclist",
                        principalColumn: "CyclistID");
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    SalaryID = table.Column<int>(type: "int", nullable: false),
                    CyclistID = table.Column<int>(type: "int", nullable: true),
                    Period = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Salary__4BE204B7D993DC3B", x => x.SalaryID);
                    table.ForeignKey(
                        name: "FK__Salary__CyclistI__282DF8C2",
                        column: x => x.CyclistID,
                        principalTable: "Cyclist",
                        principalColumn: "CyclistID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    DishID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__D3B9D30CE2DF5489", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK__OrderDeta__DishI__1EA48E88",
                        column: x => x.DishID,
                        principalTable: "Dish",
                        principalColumn: "DishID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__1DB06A4F",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    TripID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    CyclistID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trip__51DC711E5362DF4F", x => x.TripID);
                    table.ForeignKey(
                        name: "FK__Trip__CyclistID__22751F6C",
                        column: x => x.CyclistID,
                        principalTable: "Cyclist",
                        principalColumn: "CyclistID");
                    table.ForeignKey(
                        name: "FK__Trip__OrderID__2180FB33",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "TripStops",
                columns: table => new
                {
                    TripStopsID = table.Column<int>(type: "int", nullable: false),
                    TripID = table.Column<int>(type: "int", nullable: true),
                    StopAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ActionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    StopTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TripStop__A60692044ADB175B", x => x.TripStopsID);
                    table.ForeignKey(
                        name: "FK__TripStops__TripI__25518C17",
                        column: x => x.TripID,
                        principalTable: "Trip",
                        principalColumn: "TripID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dish_CookID",
                table: "Dish",
                column: "CookID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerID",
                table: "Order",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_DishID",
                table: "OrderDetail",
                column: "DishID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderID",
                table: "OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_CookID",
                table: "Rating",
                column: "CookID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_CustomerID",
                table: "Rating",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_CyclistID",
                table: "Rating",
                column: "CyclistID");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_CyclistID",
                table: "Salary",
                column: "CyclistID");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_CyclistID",
                table: "Trip",
                column: "CyclistID");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_OrderID",
                table: "Trip",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_TripID",
                table: "TripStops",
                column: "TripID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "TripStops");

            migrationBuilder.DropTable(
                name: "Dish");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Cook");

            migrationBuilder.DropTable(
                name: "Cyclist");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
