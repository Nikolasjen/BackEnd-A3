using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;

public partial class Trip
{
    public int TripId { get; set; }

    public int? OrderId { get; set; }

    public int? CyclistId { get; set; }

    public virtual Cyclist? Cyclist { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ICollection<TripStop> TripStops { get; set; } = new List<TripStop>();
}
