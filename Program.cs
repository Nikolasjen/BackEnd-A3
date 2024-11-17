using FoodAppG4.Data;
using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FoodAppG4Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CookService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CyclistService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<DishService>();
builder.Services.AddScoped<OrderDetailService>();
builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<TripStopService>();
builder.Services.AddScoped<SalaryService>();
builder.Services.AddScoped<RatingService>();

builder.Services.AddScoped<QueryService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
