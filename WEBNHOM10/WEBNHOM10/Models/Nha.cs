using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class Nha
{
    public int MaNha { get; set; }

    [DisplayName("Tên nhà")]
    public string? TenNha { get; set; }

    public virtual ICollection<Phong> Phongs { get; } = new List<Phong>();
}
