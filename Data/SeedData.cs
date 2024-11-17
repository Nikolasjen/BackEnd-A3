using System.Security.Claims;
using FoodAppG4.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodAppG4.Data
{
    public class SeedData
    {

        public static void SeedUsers(UserManager<ApiUser> userManager)
        {
            const string adminEmail = "Admin@localhost";
            const string adminPassword = "Secret7$";
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
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
                    var claim = new Claim("IsAdmin", "true");
                    var claimAdded = userManager.AddClaimAsync(adminUser, claim).Result;
                }
            }
        }
    }
}
