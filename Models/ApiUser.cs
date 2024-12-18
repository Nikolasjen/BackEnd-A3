using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FoodAppG4.Models
{
    public class ApiUser: IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }
        public int? CookId { get; set; } = null;
        public int? CyclistId { get; set; } = null;
        
    }
}