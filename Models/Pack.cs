using System;
using System.Collections.Generic;

namespace Lab4.Models;

public partial class Pack
{
    public int PackId { get; set; }

    public string PackName { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<PlaceInPack> PlaceInPacks { get; set; } = new List<PlaceInPack>();

    public virtual User? User { get; set; }
}
