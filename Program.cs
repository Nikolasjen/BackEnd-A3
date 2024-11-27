using FoodAppG4.Data;
using FoodAppG4.Models;
using FoodAppG4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using MongoDB.Driver;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Host.UseSerilog();

            // Register MongoDB client
            var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB");
            builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnectionString));

            // Add services to the container.
            builder.Services.AddDbContext<FoodAppG4Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register application services
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
            builder.Services.AddScoped<LogService>();

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

            // Authorization Policies...
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

            // Add controllers
            builder.Services.AddControllers();

            // Swagger Configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer prefix",
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


            // Ensure the app listens on all network interfaces
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80); // Listen on port 80 on all network interfaces
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                // Get the service provider
                var serviceProvider = scope.ServiceProvider;

                // Apply migrations at startup
                var dbContext = serviceProvider.GetRequiredService<FoodAppG4Context>();
                dbContext.Database.Migrate();

                // Seed the database with users
                var userManager = serviceProvider.GetService<UserManager<ApiUser>>();
                if (userManager != null)
                {
                    await SeedData.SeedUsersAsync(userManager);
                }
                else
                {
                    throw new Exception("Unable to get UserManager!");
                }
            }

            app.MapControllers();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: {0}", ex.Message);
        }
        finally
        {
            Console.WriteLine("Application shutting down...");
        }
    }
}
