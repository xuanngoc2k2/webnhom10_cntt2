using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEBNHOM10.Models;

public partial class Lop
{
    [DisplayName("Mã lớp")]
    public int MaLop { get; set; }
    [Required(ErrorMessage = "Tên lớp không được để trống")]
    [DisplayName("Tên lớp")]
    public string? TenLop { get; set; }

    public int? MaKhoa { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();
}
