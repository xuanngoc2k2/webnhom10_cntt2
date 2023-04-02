using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class ChiTietHoaDon
{
    public int MaHoaDon { get; set; }

    public int MaPhong { get; set; }

    public int? Tiendien { get; set; }

    public int? Tiennuoc { get; set; }

    public int? Tiendichvu { get; set; }

    public int? Tienphong { get; set; }

    public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;

    public virtual Phong MaPhongNavigation { get; set; } = null!;
}
