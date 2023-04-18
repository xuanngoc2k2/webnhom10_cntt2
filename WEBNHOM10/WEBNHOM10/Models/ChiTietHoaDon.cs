using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class ChiTietHoaDon
{
    public int MaHoaDon { get; set; }
    [Required]
    public int MaPhong { get; set; }
    [Required]
    [DisplayName("Tiền điện")]
    public int Tiendien { get; set; }
    [DisplayName("Tiền nước")]
    [Required]
    public int Tiennuoc { get; set; }
    [DisplayName("Tiền dịch vụ")]
    [Required]
    public int Tiendichvu { get; set; }
    [DisplayName("Tiền phòng")]
    public int? Tienphong { get; set; }

    public virtual HoaDon MaHoaDonNavigation { get; set; } = null!;

    public virtual Phong MaPhongNavigation { get; set; } = null!;
}
