using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class TaiKhoan
{
    public int IdUser { get; set; }

    public string? Username { get; set; }

    public string? Pass { get; set; }

    public int? LoaiTk { get; set; }
}
