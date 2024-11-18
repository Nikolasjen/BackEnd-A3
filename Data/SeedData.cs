using System.Security.Claims;
using FoodAppG4.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodAppG4.Data
{
    public class SeedData
    {

        public static void SeedUsers(UserManager<ApiUser> userManager)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));

            // var user0_del = userManager.FindByNameAsync("Admin@localhost").Result;
            // if (user0_del != null)
            //     userManager.DeleteAsync(user0_del);



            // Add Admin User
            const string adminEmail = "Admin@localhost";
            const string adminPassword = "Secret7$";
            if (userManager.FindByNameAsync(adminEmail).Result == null)
            {
                var user = new ApiUser();
                user.UserName = adminEmail;
                user.Email = adminEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, adminPassword).Result;
                if (result.Succeeded)
                {
                    var adminUser = userManager.FindByNameAsync(adminEmail).Result;
                    if (adminUser == null)
                    {
                        throw new Exception("Failed to find user1");
                    }
                    var claim = new Claim("IsAdmin", "true");
                    var claimAdded = userManager.AddClaimAsync(adminUser, claim).Result;

                    if (!claimAdded.Succeeded)
                    {
                        throw new Exception("Failed to add claim to admin user");
                    }
                }
            }

            // Add Manager User
            const string managerEmail = "Manager@localhost";
            const string managerPassword = "Secret7$";
            if (userManager.FindByNameAsync(managerEmail).Result == null)
            {
                var user2 = new ApiUser();
                user2.UserName = managerEmail;
                user2.Email = managerEmail;
                user2.EmailConfirmed = true;
                IdentityResult result2 = userManager.CreateAsync(user2, managerPassword).Result;
                if (result2.Succeeded)
                {
                    var managerUser = userManager.FindByNameAsync(managerEmail).Result;
                    if (managerUser == null)
                    {
                        throw new Exception("Failed to find manager");
                    }
                    var claim = new Claim("IsManager", "true");
                    var claimAdded = userManager.AddClaimAsync(managerUser, claim).Result;

                    if (!claimAdded.Succeeded)
                    {
                        throw new Exception("Failed to add claim to manager user");
                    }
                }
            }

            // Add Cook User
            const string cookEmail = "cook@localhost";
            const string cookPassword = "Secret7$";
            if (userManager.FindByNameAsync(cookEmail).Result == null)
            {
                var user3 = new ApiUser();
                user3.UserName = cookEmail;
                user3.Email = cookEmail;
                user3.EmailConfirmed = true;
                user3.CookId = 1;
                IdentityResult result3 = userManager.CreateAsync(user3, cookPassword).Result;
                if (result3.Succeeded)
                {
                    var cookUser = userManager.FindByNameAsync(cookEmail).Result;
                    if (cookUser == null)
                    {
                        throw new Exception("Failed to find cook user");
                    }
                    var claim = new Claim("IsCook", "true");
                    var claim2 = new Claim("CookId", user3.CookId.ToString() ?? string.Empty);
                    var claimAdded = userManager.AddClaimAsync(cookUser, claim).Result;
                    var claimAdded2 = userManager.AddClaimAsync(cookUser, claim2).Result;

                    if (!claimAdded.Succeeded || !claimAdded2.Succeeded)
                    {
                        throw new Exception("Failed to add claims to cook user");
                    }
                }
            }

            // Add Cyclist User
            const string cyclistEmail = "cyclist@localhost";
            const string cyclistPassword = "Secret7$";
            if (userManager.FindByNameAsync(cyclistEmail).Result == null)
            {
                var user3 = new ApiUser();
                user3.UserName = cyclistEmail;
                user3.Email = cyclistEmail;
                user3.EmailConfirmed = true;
                user3.CyclistId = 1;
                IdentityResult result3 = userManager.CreateAsync(user3, cyclistPassword).Result;
                if (result3.Succeeded)
                {
                    var cyclistUser = userManager.FindByNameAsync(cyclistEmail).Result;
                    if (cyclistUser == null)
                    {
                        throw new Exception("Failed to find cyclist user");
                    }
                    var claim = new Claim("IsCyclist", "true");
                    var claim2 = new Claim("CyclistId", user3.CyclistId.ToString() ?? string.Empty);
                    var claimAdded = userManager.AddClaimAsync(cyclistUser, claim).Result;
                    var claimAdded2 = userManager.AddClaimAsync(cyclistUser, claim2).Result;

                    if (!claimAdded.Succeeded || !claimAdded2.Succeeded)
                    {
                        throw new Exception("Failed to add claims to cyclist user");
                    }
                }
            }

            // Add Cyclist User
            const string noRightsEmail = "noRights@localhost";
            const string noRightsPassword = "Secret7$";
            if (userManager.FindByNameAsync(noRightsEmail).Result == null)
            {
                var user3 = new ApiUser();
                user3.UserName = noRightsEmail;
                user3.Email = noRightsEmail;
                user3.EmailConfirmed = true;
                IdentityResult result3 = userManager.CreateAsync(user3, noRightsPassword).Result;
                if (result3.Succeeded)
                {
                    var cyclistUser = userManager.FindByNameAsync(noRightsEmail).Result;
                    if (cyclistUser == null)
                    {
                        throw new Exception("Failed to find noRights user");
                    }
                }
            }
        }
    }
}
