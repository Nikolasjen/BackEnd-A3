using System;
using System.Collections.Generic;

namespace FoodAppG4.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int? CyclistId { get; set; }

    public string? Period { get; set; }

    public int? Hours { get; set; }

    public virtual Cyclist? Cyclist { get; set; }
}
