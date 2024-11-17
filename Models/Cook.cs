using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;

public partial class Cook
{
    public int CookId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public bool PassedCourse { get; set; } // New attribute for food safety course

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
