using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class Khoa
{
    public int MaKhoa { get; set; }

    public string? TenKhoa { get; set; }

    public virtual ICollection<Lop> Lops { get; } = new List<Lop>();
}
