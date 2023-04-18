using Microsoft.Build.Framework;

namespace WEBNHOM10.Models
{
    public class HoaDonViewModel
    {
        [Required]
        public HoaDon HoaDon { get; set; }
        [Required]
        public ChiTietHoaDon ChiTietHoaDon { get; set; }
    }
}
