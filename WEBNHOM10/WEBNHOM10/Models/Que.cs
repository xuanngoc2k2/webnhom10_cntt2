using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class Que
{
    [DisplayName("Mã quê")]
    public int MaQue { get; set; }

    [DisplayName("Tên quê")]
    public string? TenQue { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();
}
