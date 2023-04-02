using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class ThietBi
{
    public int MaThietBi { get; set; }

    public string? TenThietBi { get; set; }

    public int? MaPhong { get; set; }

    public string? AnhThietBi { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }
}
