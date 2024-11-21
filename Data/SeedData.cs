using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodAppG4.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodAppG4.Data
{
    public static class SeedData
    {
        /// <summary>
        /// Asynchronously seeds users into the database. If a user already exists, it is removed before being re-created.
        /// </summary>
        /// <param name="userManager">The UserManager instance for managing users.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public static async Task SeedUsersAsync(UserManager<ApiUser> userManager)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            // Define users to seed
            var users = new[]
            {
                new
                {
                    Email = "Admin@localhost",
                    Password = "Secret7$",
                    Claims = new[]
                    {
                        new Claim("IsAdmin", "true")
                    }
                },
                new
                {
                    Email = "Manager@localhost",
                    Password = "Secret7$",
                    Claims = new[]
                    {
                        new Claim("IsManager", "true")
                    }
                },
                new
                {
                    Email = "cook@localhost",
                    Password = "Secret7$",
                    Claims = new[]
                    {
                        new Claim("IsCook", "true"),
                        new Claim("CookId", "1")
                    }
                },
                new
                {
                    Email = "cyclist@localhost",
                    Password = "Secret7$",
                    Claims = new[]
                    {
                        new Claim("IsCyclist", "true"),
                        new Claim("CyclistId", "1")
                    }
                },
                new
                {
                    Email = "noRights@localhost",
                    Password = "Secret7$",
                    Claims = Array.Empty<Claim>() // No special claims
                }
            };

            foreach (var userInfo in users)
            {
                // Attempt to find the user by email
                var existingUser = await userManager.FindByEmailAsync(userInfo.Email);
                if (existingUser != null)
                {
                    // Delete the existing user
                    var deleteResult = await userManager.DeleteAsync(existingUser);
                    if (!deleteResult.Succeeded)
                    {
                        throw new Exception($"Failed to delete existing user: {userInfo.Email}. Errors: {string.Join(", ", deleteResult.Errors.Select(e => e.Description))}");
                    }
                }

                // Create a new user
                var newUser = new ApiUser
                {
                    UserName = userInfo.Email,
                    Email = userInfo.Email,
                    EmailConfirmed = true
                };

                // For users with specific IDs (e.g., CookId, CyclistId), set them accordingly
                if (userInfo.Claims.Any(c => c.Type == "CookId"))
                {
                    newUser.CookId = int.Parse(userInfo.Claims.First(c => c.Type == "CookId").Value);
                }

                if (userInfo.Claims.Any(c => c.Type == "CyclistId"))
                {
                    newUser.CyclistId = int.Parse(userInfo.Claims.First(c => c.Type == "CyclistId").Value);
                }

                var createResult = await userManager.CreateAsync(newUser, userInfo.Password);
                if (!createResult.Succeeded)
                {
                    throw new Exception($"Failed to create user: {userInfo.Email}. Errors: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }

                // Retrieve the newly created user
                var createdUser = await userManager.FindByEmailAsync(userInfo.Email);
                if (createdUser == null)
                {
                    throw new Exception($"Failed to retrieve user after creation: {userInfo.Email}");
                }

                // Add claims to the user
                if (userInfo.Claims.Any())
                {
                    var addClaimsResult = await userManager.AddClaimsAsync(createdUser, userInfo.Claims);
                    if (!addClaimsResult.Succeeded)
                    {
                        throw new Exception($"Failed to add claims to user: {userInfo.Email}. Errors: {string.Join(", ", addClaimsResult.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }
    }
}
