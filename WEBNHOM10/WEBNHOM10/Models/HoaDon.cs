using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class HoaDon
{
    public int MaHoaDon { get; set; }

    public DateTime? HanThanhToan { get; set; }

    public int? TongTien { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; } = new List<ChiTietHoaDon>();
}
