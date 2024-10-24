using System;
using System.Collections.Generic;
using Lab4.Models;

namespace Lab4;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? PlaceId { get; set; }

    public byte Grade { get; set; }

    public string? ReviewText { get; set; }

    public virtual Place? Place { get; set; }

    public virtual User? User { get; set; }
}
