﻿// <auto-generated />
using System;
using FoodAppG4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodAppG4.Migrations
{
    [DbContext(typeof(FoodAppG4Context))]
    [Migration("20241117085735_Identity")]
    partial class Identity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodAppG4.Models.ApiUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("FoodAppG4.Models.Cook", b =>
                {
                    b.Property<int>("CookId")
                        .HasColumnType("int")
                        .HasColumnName("CookID");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("PassedCourse")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("CookId")
                        .HasName("PK__Cook__E51807016A4A9DE2");

                    b.ToTable("Cook", (string)null);

                    b.HasData(
                        new
                        {
                            CookId = 1,
                            Address = "Finlandsgade 17, 8200 Aarhus N",
                            Name = "Noah",
                            PassedCourse = true,
                            Phone = "+45 71555080"
                        },
                        new
                        {
                            CookId = 2,
                            Address = "Ny Munkegade 118, 8200 Aarhus N",
                            Name = "Helle",
                            PassedCourse = true,
                            Phone = "+45 71555081"
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PaymentInfo")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Payment_Info");

                    b.HasKey("CustomerId")
                        .HasName("PK__Customer__A4AE64B8FF097CAC");

                    b.ToTable("Customer", (string)null);

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "Finsensgade 1493, 8000 Aarhus",
                            Name = "Knuth",
                            PaymentInfo = "Card"
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.Cyclist", b =>
                {
                    b.Property<int>("CyclistId")
                        .HasColumnType("int")
                        .HasColumnName("CyclistID");

                    b.Property<string>("BikeType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("HourlyRate")
                        .HasColumnType("decimal(10, 2)")
                        .HasColumnName("Hourly_rate");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CyclistId")
                        .HasName("PK__Cyclist__A3CBF9E0134B5CEE");

                    b.ToTable("Cyclist", (string)null);

                    b.HasData(
                        new
                        {
                            CyclistId = 1,
                            BikeType = "Electric Bike",
                            HourlyRate = 100.00m,
                            Name = "Star"
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.Dish", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int")
                        .HasColumnName("DishID");

                    b.Property<DateTime?>("AvailableFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AvailableTo")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CookId")
                        .HasColumnType("int")
                        .HasColumnName("CookID");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("DishId")
                        .HasName("PK__Dish__18834F7058F04B29");

                    b.HasIndex("CookId");

                    b.ToTable("Dish", (string)null);

                    b.HasData(
                        new
                        {
                            DishId = 1,
                            AvailableFrom = new DateTime(2024, 9, 15, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            AvailableTo = new DateTime(2024, 9, 15, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            CookId = 1,
                            Name = "Pasta",
                            Price = 30.00m
                        },
                        new
                        {
                            DishId = 2,
                            AvailableFrom = new DateTime(2024, 9, 15, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            AvailableTo = new DateTime(2024, 9, 15, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            CookId = 1,
                            Name = "Romkugle",
                            Price = 3.00m
                        },
                        new
                        {
                            DishId = 3,
                            AvailableFrom = new DateTime(2024, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            AvailableTo = new DateTime(2024, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            CookId = 2,
                            Name = "Lemonade",
                            Price = 15.00m
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId")
                        .HasName("PK__Order__C3905BAFF98CAB4A");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            CustomerId = 1,
                            OrderDate = new DateTime(2024, 11, 17, 9, 57, 34, 925, DateTimeKind.Local).AddTicks(3501)
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int")
                        .HasColumnName("OrderDetailID");

                    b.Property<int?>("DishId")
                        .HasColumnType("int")
                        .HasColumnName("DishID");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId")
                        .HasName("PK__OrderDet__D3B9D30CE2DF5489");

                    b.HasIndex("DishId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail", (string)null);

                    b.HasData(
                        new
                        {
                            OrderDetailId = 1,
                            DishId = 1,
                            OrderId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderDetailId = 2,
                            DishId = 2,
                            OrderId = 1,
                            Quantity = 4
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .HasColumnType("int")
                        .HasColumnName("RatingID");

                    b.Property<int?>("CookId")
                        .HasColumnType("int")
                        .HasColumnName("CookID");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int?>("CyclistId")
                        .HasColumnType("int")
                        .HasColumnName("CyclistID");

                    b.Property<int?>("DeliveryScore")
                        .HasColumnType("int");

                    b.Property<int?>("FoodScore")
                        .HasColumnType("int");

                    b.HasKey("RatingId")
                        .HasName("PK__Rating__FCCDF85C1B6CDE4B");

                    b.HasIndex("CookId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CyclistId");

                    b.ToTable("Rating", (string)null);

                    b.HasData(
                        new
                        {
                            RatingId = 1,
                            CookId = 1,
                            CustomerId = 1,
                            CyclistId = 1,
                            DeliveryScore = 5,
                            FoodScore = 5
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.Salary", b =>
                {
                    b.Property<int>("SalaryId")
                        .HasColumnType("int")
                        .HasColumnName("SalaryID");

                    b.Property<int?>("CyclistId")
                        .HasColumnType("int")
                        .HasColumnName("CyclistID");

                    b.Property<int?>("Hours")
                        .HasColumnType("int");

                    b.Property<string>("Period")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("SalaryId")
                        .HasName("PK__Salary__4BE204B7D993DC3B");

                    b.HasIndex("CyclistId");

                    b.ToTable("Salary", (string)null);
                });

            modelBuilder.Entity("FoodAppG4.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .HasColumnType("int")
                        .HasColumnName("TripID");

                    b.Property<int?>("CyclistId")
                        .HasColumnType("int")
                        .HasColumnName("CyclistID");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.HasKey("TripId")
                        .HasName("PK__Trip__51DC711E5362DF4F");

                    b.HasIndex("CyclistId");

                    b.HasIndex("OrderId");

                    b.ToTable("Trip", (string)null);

                    b.HasData(
                        new
                        {
                            TripId = 1,
                            CyclistId = 1,
                            OrderId = 1
                        });
                });

            modelBuilder.Entity("FoodAppG4.Models.TripStop", b =>
                {
                    b.Property<int>("TripStopsId")
                        .HasColumnType("int")
                        .HasColumnName("TripStopsID");

                    b.Property<string>("ActionType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StopAddress")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("StopTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TripId")
                        .HasColumnType("int")
                        .HasColumnName("TripID");

                    b.HasKey("TripStopsId")
                        .HasName("PK__TripStop__A60692044ADB175B");

                    b.HasIndex("TripId");

                    b.ToTable("TripStops");

                    b.HasData(
                        new
                        {
                            TripStopsId = 1,
                            ActionType = "Picked Up",
                            StopAddress = "Finlandsgade 17, 8200 Aarhus N",
                            StopTime = new DateTime(2024, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            TripId = 1
                        },
                        new
                        {
                            TripStopsId = 2,
                            ActionType = "Delivered",
                            StopAddress = "Finsensgade 1493, 8000 Aarhus",
                            StopTime = new DateTime(2024, 9, 15, 12, 16, 0, 0, DateTimeKind.Unspecified),
                            TripId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FoodAppG4.Models.Dish", b =>
                {
                    b.HasOne("FoodAppG4.Models.Cook", "Cook")
                        .WithMany("Dishes")
                        .HasForeignKey("CookId")
                        .HasConstraintName("FK__Dish__CookID__1AD3FDA4");

                    b.Navigation("Cook");
                });

            modelBuilder.Entity("FoodAppG4.Models.Order", b =>
                {
                    b.HasOne("FoodAppG4.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Order__CustomerI__17F790F9");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FoodAppG4.Models.OrderDetail", b =>
                {
                    b.HasOne("FoodAppG4.Models.Dish", "Dish")
                        .WithMany("OrderDetails")
                        .HasForeignKey("DishId")
                        .HasConstraintName("FK__OrderDeta__DishI__1EA48E88");

                    b.HasOne("FoodAppG4.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderDeta__Order__1DB06A4F");

                    b.Navigation("Dish");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FoodAppG4.Models.Rating", b =>
                {
                    b.HasOne("FoodAppG4.Models.Cook", "Cook")
                        .WithMany("Ratings")
                        .HasForeignKey("CookId")
                        .HasConstraintName("FK__Rating__CookID__2B0A656D");

                    b.HasOne("FoodAppG4.Models.Customer", "Customer")
                        .WithMany("Ratings")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Rating__Customer__2BFE89A6");

                    b.HasOne("FoodAppG4.Models.Cyclist", "Cyclist")
                        .WithMany("Ratings")
                        .HasForeignKey("CyclistId")
                        .HasConstraintName("FK__Rating__CyclistI__2CF2ADDF");

                    b.Navigation("Cook");

                    b.Navigation("Customer");

                    b.Navigation("Cyclist");
                });

            modelBuilder.Entity("FoodAppG4.Models.Salary", b =>
                {
                    b.HasOne("FoodAppG4.Models.Cyclist", "Cyclist")
                        .WithMany("Salaries")
                        .HasForeignKey("CyclistId")
                        .HasConstraintName("FK__Salary__CyclistI__282DF8C2");

                    b.Navigation("Cyclist");
                });

            modelBuilder.Entity("FoodAppG4.Models.Trip", b =>
                {
                    b.HasOne("FoodAppG4.Models.Cyclist", "Cyclist")
                        .WithMany("Trips")
                        .HasForeignKey("CyclistId")
                        .HasConstraintName("FK__Trip__CyclistID__22751F6C");

                    b.HasOne("FoodAppG4.Models.Order", "Order")
                        .WithMany("Trips")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__Trip__OrderID__2180FB33");

                    b.Navigation("Cyclist");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FoodAppG4.Models.TripStop", b =>
                {
                    b.HasOne("FoodAppG4.Models.Trip", "Trip")
                        .WithMany("TripStops")
                        .HasForeignKey("TripId")
                        .HasConstraintName("FK__TripStops__TripI__25518C17");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FoodAppG4.Models.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FoodAppG4.Models.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodAppG4.Models.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FoodAppG4.Models.ApiUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodAppG4.Models.Cook", b =>
                {
                    b.Navigation("Dishes");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("FoodAppG4.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("FoodAppG4.Models.Cyclist", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Salaries");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("FoodAppG4.Models.Dish", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("FoodAppG4.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("FoodAppG4.Models.Trip", b =>
                {
                    b.Navigation("TripStops");
                });
#pragma warning restore 612, 618
        }
    }
}
