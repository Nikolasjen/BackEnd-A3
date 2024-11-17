using System;
using System.Collections.Generic;
using FoodAppG4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FoodAppG4.Data;

public partial class FoodAppG4Context : IdentityDbContext<ApiUser>
{
    // public FoodAppG4Context()
    // {
    // }

    public FoodAppG4Context(DbContextOptions<FoodAppG4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cook> Cooks { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Cyclist> Cyclists { get; set; }
    public virtual DbSet<Dish> Dishes { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }
    public virtual DbSet<Salary> Salaries { get; set; }
    public virtual DbSet<Trip> Trips { get; set; }
    public virtual DbSet<TripStop> TripStops { get; set; }
    public virtual DbSet<ApiUser> ApiUsers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1433;Database=FoodApp_G4;User Id=sa;Password=G4_BadPassword;TrustServerCertificate=True");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Cooks
        modelBuilder.Entity<Cook>().HasData(
            new Cook { CookId = 1, Name = "Noah", Address = "Finlandsgade 17, 8200 Aarhus N", Phone = "+45 71555080", PassedCourse = true },
            new Cook { CookId = 2, Name = "Helle", Address = "Ny Munkegade 118, 8200 Aarhus N", Phone = "+45 71555081", PassedCourse = true }
        );

        // Seed data for Cyclist
        modelBuilder.Entity<Cyclist>().HasData(
            new Cyclist { CyclistId = 1, Name = "Star", HourlyRate = 100.00M, BikeType = "Electric Bike" }
        );

        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, Name = "Knuth", Address = "Finsensgade 1493, 8000 Aarhus", PaymentInfo = "Card" }
        );

        // Seed data for Dishes
        modelBuilder.Entity<Dish>().HasData(
            new Dish { DishId = 1, CookId = 1, Name = "Pasta", Price = 30.00M, AvailableFrom = new DateTime(2024, 9, 15, 11, 30, 0), AvailableTo = new DateTime(2024, 9, 15, 12, 30, 0) },
            new Dish { DishId = 2, CookId = 1, Name = "Romkugle", Price = 3.00M, AvailableFrom = new DateTime(2024, 9, 15, 8, 0, 0), AvailableTo = new DateTime(2024, 9, 15, 12, 30, 0) },
            new Dish { DishId = 3, CookId = 2, Name = "Lemonade", Price = 15.00M, AvailableFrom = new DateTime(2024, 9, 15, 14, 0, 0), AvailableTo = new DateTime(2024, 9, 15, 16, 0, 0) }
        );

        // Seed data for Orders
        modelBuilder.Entity<Order>().HasData(
            new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now }
        );

        // Seed data for OrderDetails
        modelBuilder.Entity<OrderDetail>().HasData(
            new OrderDetail { OrderDetailId = 1, OrderId = 1, DishId = 1, Quantity = 2 }, // 2 Pasta from Noah's kitchen
            new OrderDetail { OrderDetailId = 2, OrderId = 1, DishId = 2, Quantity = 4 }  // 4 Romkugle from Noah's kitchen
        );

        // Seed data for Trips
        modelBuilder.Entity<Trip>().HasData(
            new Trip { TripId = 1, OrderId = 1, CyclistId = 1 }
        );

        // Seed data for TripStops
        modelBuilder.Entity<TripStop>().HasData(
            new TripStop { TripStopsId = 1, TripId = 1, StopAddress = "Finlandsgade 17, 8200 Aarhus N", ActionType = "Picked Up", StopTime = new DateTime(2024, 9, 15, 12, 0, 0) },
            new TripStop { TripStopsId = 2, TripId = 1, StopAddress = "Finsensgade 1493, 8000 Aarhus", ActionType = "Delivered", StopTime = new DateTime(2024, 9, 15, 12, 16, 0) }
        );

        // Seed data for Ratings
        modelBuilder.Entity<Rating>().HasData(
            new Rating { RatingId = 1, CookId = 1, CustomerId = 1, CyclistId = 1, DeliveryScore = 5, FoodScore = 5 }
        );

        // modelBuilder.Entity<ApiUser>(entity =>
        // {
        //     entity.HasNoKey();

        //     entity.Property(e => e.FullName)
        //         .HasMaxLength(100)
        //         .IsUnicode(false);
        
        //     entity.ToTable("ApiUser");
        // });

        modelBuilder.Entity<Cook>(entity =>
        {
            entity.HasKey(e => e.CookId).HasName("PK__Cook__E51807016A4A9DE2");

            entity.ToTable("Cook");

            entity.Property(e => e.CookId)
                .ValueGeneratedNever()
                .HasColumnName("CookID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PassedCourse)
                .HasColumnType("bit")
                .HasDefaultValue(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8FF097CAC");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentInfo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Payment_Info");
        });

        modelBuilder.Entity<Cyclist>(entity =>
        {
            entity.HasKey(e => e.CyclistId).HasName("PK__Cyclist__A3CBF9E0134B5CEE");

            entity.ToTable("Cyclist");

            entity.Property(e => e.CyclistId)
                .ValueGeneratedNever()
                .HasColumnName("CyclistID");
            entity.Property(e => e.BikeType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HourlyRate)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Hourly_rate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PK__Dish__18834F7058F04B29");

            entity.ToTable("Dish");

            entity.Property(e => e.DishId)
                .ValueGeneratedNever()
                .HasColumnName("DishID");
            entity.Property(e => e.CookId).HasColumnName("CookID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cook).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.CookId)
                .HasConstraintName("FK__Dish__CookID__1AD3FDA4");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAFF98CAB4A");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Order__CustomerI__17F790F9");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CE2DF5489");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .ValueGeneratedNever()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.DishId).HasColumnName("DishID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Dish).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.DishId)
                .HasConstraintName("FK__OrderDeta__DishI__1EA48E88");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__1DB06A4F");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__FCCDF85C1B6CDE4B");

            entity.ToTable("Rating");

            entity.Property(e => e.RatingId)
                .ValueGeneratedNever()
                .HasColumnName("RatingID");
            entity.Property(e => e.CookId).HasColumnName("CookID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.CyclistId).HasColumnName("CyclistID");

            entity.HasOne(d => d.Cook).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CookId)
                .HasConstraintName("FK__Rating__CookID__2B0A656D");

            entity.HasOne(d => d.Customer).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Rating__Customer__2BFE89A6");

            entity.HasOne(d => d.Cyclist).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CyclistId)
                .HasConstraintName("FK__Rating__CyclistI__2CF2ADDF");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Salary__4BE204B7D993DC3B");

            entity.ToTable("Salary");

            entity.Property(e => e.SalaryId)
                .ValueGeneratedNever()
                .HasColumnName("SalaryID");
            entity.Property(e => e.CyclistId).HasColumnName("CyclistID");
            entity.Property(e => e.Period)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cyclist).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.CyclistId)
                .HasConstraintName("FK__Salary__CyclistI__282DF8C2");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trip__51DC711E5362DF4F");

            entity.ToTable("Trip");

            entity.Property(e => e.TripId)
                .ValueGeneratedNever()
                .HasColumnName("TripID");
            entity.Property(e => e.CyclistId).HasColumnName("CyclistID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.Cyclist).WithMany(p => p.Trips)
                .HasForeignKey(d => d.CyclistId)
                .HasConstraintName("FK__Trip__CyclistID__22751F6C");

            entity.HasOne(d => d.Order).WithMany(p => p.Trips)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Trip__OrderID__2180FB33");
        });

        modelBuilder.Entity<TripStop>(entity =>
        {
            entity.HasKey(e => e.TripStopsId).HasName("PK__TripStop__A60692044ADB175B");

            entity.Property(e => e.TripStopsId)
                .ValueGeneratedNever()
                .HasColumnName("TripStopsID");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StopAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.HasOne(d => d.Trip).WithMany(p => p.TripStops)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK__TripStops__TripI__25518C17");
        });

        // OnModelCreatingPartial(modelBuilder);
    }

    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
