using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class Nha
{
    public int MaNha { get; set; }

    public string? TenNha { get; set; }

    public virtual ICollection<Phong> Phongs { get; } = new List<Phong>();
}
