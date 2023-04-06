using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEBNHOM10.Models;

public partial class Que
{
    [DisplayName("Mã quê")]
    public int MaQue { get; set; }

    [Required(ErrorMessage = "Tên quê không được để trống")]
    [DisplayName("Tên quê")]
    public string? TenQue { get; set; }

    public virtual ICollection<SinhVien> SinhViens { get; } = new List<SinhVien>();
}
