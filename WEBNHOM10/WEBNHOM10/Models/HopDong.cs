using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class HopDong
{
    public int MaHopDong { get; set; }

    public DateTime NgayKi { get; set; }

    public DateTime NgayHetHan { get; set; }

    public int? GiaThue { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();
}
