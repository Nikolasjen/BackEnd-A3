using FoodAppG4.Data;
using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

// Add Identity
builder.Services.AddIdentity<ApiUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<FoodAppG4Context>();

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{ // Enable JWT bearer authentication
    options.TokenValidationParameters = new TokenValidationParameters
    { // Configure the JWT validation, issuing, and lifetime settings
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
    System.Text.Encoding.UTF8.GetBytes(
    builder.Configuration["JWT:SigningKey"]))
    };
});

// Policies...
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "true")));

    options.AddPolicy("AdminOrManagerOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "true") ||
            context.User.HasClaim(c => c.Type == "IsManager" && c.Value == "true")));

    options.AddPolicy("AdminOrCookOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "true") ||
            context.User.HasClaim(c => c.Type == "IsCook" && c.Value == "true")));

    options.AddPolicy("AdminOrCyclistOnly", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "IsAdmin" && c.Value == "true") ||
            context.User.HasClaim(c => c.Type == "IsCyclist" && c.Value == "true")));
    
});


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // options.ParameterFilter<SortColumnFilter>();
    // options.ParameterFilter<SortOrderFilter>();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetService<UserManager<ApiUser>>();
    if (userManager != null)
        SeedData.SeedUsers(userManager);
    else throw new Exception("Unable to get UserManager!");
}

app.MapControllers();

app.Run();
