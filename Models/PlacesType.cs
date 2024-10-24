using System;
using System.Collections.Generic;
using Lab4.Models;

namespace Lab4;

public partial class PlacesType
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();
}
