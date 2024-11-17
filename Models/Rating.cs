using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? CookId { get; set; }

    public int? CustomerId { get; set; }

    public int? CyclistId { get; set; }

    public int? DeliveryScore { get; set; }

    public int? FoodScore { get; set; }

    public virtual Cook? Cook { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Cyclist? Cyclist { get; set; }
}
