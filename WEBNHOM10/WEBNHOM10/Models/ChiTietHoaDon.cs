using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class ChiTietHoaDon
{
    public int MaHoaDon { get; set; }

    public int MaPhong { get; set; }

    [DisplayName("Tiền điện")]
    public int? Tiendien { get; set; }
    [DisplayName("Tiền nước")]
    public int? Tiennuoc { get; set; }
    [DisplayName("Tiền dịch vụ")]
    public int? Tiendichvu { get; set; }
    [DisplayName("Tiền phòng")]
    public int? Tienphong { get; set; }

    public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;

    public virtual Phong MaPhongNavigation { get; set; } = null!;
}
