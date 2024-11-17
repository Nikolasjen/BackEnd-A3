using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;

public partial class Cyclist
{
    public int CyclistId { get; set; }

    public string? Name { get; set; }

    public decimal? HourlyRate { get; set; }

    public string? BikeType { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
