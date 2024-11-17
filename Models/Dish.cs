using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;


public partial class Dish
{
    public int DishId { get; set; }

    public int? CookId { get; set; }

    public string? Name { get; set; }

    [NonNegativePrice]
    public decimal? Price { get; set; }

    public DateTime? AvailableFrom { get; set; } // AvailableFrom was already DateTime so no migration is needed 

    public DateTime? AvailableTo { get; set; }   // AvailableTo was already DateTime so no migration is needed

    public virtual Cook? Cook { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

