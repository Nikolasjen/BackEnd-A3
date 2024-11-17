using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;

public partial class TripStop
{
    public int TripStopsId { get; set; }

    public int? TripId { get; set; }

    public string? StopAddress { get; set; }

    public string? ActionType { get; set; }

    public DateTime? StopTime { get; set; }

    public virtual Trip? Trip { get; set; }
}
