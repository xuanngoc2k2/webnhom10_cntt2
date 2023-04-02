using System;
using System.Collections.Generic;

namespace WEBNHOM10.Models;

public partial class CtanhPhong
{
    public int MaPhong { get; set; }

    public string LinkAnh { get; set; } = null!;

    public virtual Phong MaPhongNavigation { get; set; } = null!;
}
