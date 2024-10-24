using System;
using System.Collections.Generic;

namespace Lab4.ViewModels;

public partial class UserPack
{
    public int UserId { get; set; }

    public string UserLogin { get; set; } = null!;

    public string PackName { get; set; } = null!;
}
