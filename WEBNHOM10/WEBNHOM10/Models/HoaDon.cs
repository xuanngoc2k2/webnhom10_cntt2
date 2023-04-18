using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class HoaDon
{
    [Required]
    [DisplayName("Mã hóa đơn")]
    public int MaHoaDon { get; set; }
    [DisplayName("Hạn thanh toán")]
    [Required]
    public DateTime HanThanhToan { get; set; }

    [DisplayName("Tổng tiền")]
    public int? TongTien { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; } = new List<ChiTietHoaDon>();
}
