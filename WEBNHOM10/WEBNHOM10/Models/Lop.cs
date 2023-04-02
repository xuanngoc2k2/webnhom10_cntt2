using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WEBNHOM10.Models;

public partial class Lop
{
    [DisplayName("Mã lớp")]
    public int MaLop { get; set; }

    [DisplayName("Tên lớp")]
    public string? TenLop { get; set; }

    public int? MaKhoa { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();
}
