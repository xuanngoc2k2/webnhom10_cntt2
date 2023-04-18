using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using WEBNHOM10.CustomValidations;

namespace WEBNHOM10.Models;

public partial class SinhVien
{/*
    [Key]*/
    [DisplayName("Mã sinh viên")]/*
    [Required(ErrorMessage ="Mã sinh viên không được để trống")]
    [MSVduynhat(ErrorMessage = "Đã tồn tại mã sinh viên này")]*/
    public int MaSinhVien { get; set; }
/*
    [Required(ErrorMessage = "Tên sinh viên không được để trống")]*//**/
    [DisplayName("Tên sinh viên")]
    public string? TenSinhVien { get; set; }
/*
    [Required(ErrorMessage = "Bạn phải thêm ngày sinh")]
    [CheckNS(ErrorMessage = "Bạn chưa đủ 18 tuổi")]*/
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    [DisplayName("Ngày sinh")]
    public DateTime? NgaySinh { get; set; }
/*
    [Required(ErrorMessage = "Giới tính không được để trống")]
    [CheckGT(ErrorMessage ="Giới tính chỉ có thể là Nam, Nữ hoặc Khác")]*/
    [DisplayName("Giới tính")]
    public string? GioiTinh { get; set; }

    [DisplayName("Số điện thoại")]
    public string? SoDienThoat { get; set; }

    [DisplayName("Mã hợp đồng")]
    public int? MaHopDong { get; set; }

    [DisplayName("Mã phòng")]
    public int? MaPhong { get; set; }

    [DisplayName("Mã lớp")]
    public int? MaLop { get; set; }

    [DisplayName("Mã quê")]
    public int? MaQue { get; set; }
/*
    [Required(ErrorMessage = "Tài khoản không được để trống")]
    [CheckTK(ErrorMessage = "Tên tài khoản đã tồn tại ")]
    [RegularExpression(@"^[A-Za-z0-9 ]*$", ErrorMessage ="Tên tài khoản không được chứa kí tự đặc biệt")]
    [MinLength(8,ErrorMessage ="Tên tài khoản phải chứa ít nhất 8 kí tự")]*/
    [DisplayName("Tài khoản")]
    public string? TaiKhoan { get; set; }
/*
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [MinLength(8,ErrorMessage ="Mật khẩu chứa ít nhất 8 kí tự")]
    [MaxLength(30,ErrorMessage ="Mật khẩu không dài quá 30 kí tự")]*/
    [DisplayName("Mật khẩu")]
    public string? MatKhau { get; set; }

    [DisplayName("Ảnh")]
    public string? Anh { get; set; }
/*
    [Required(ErrorMessage = "Số CCCD/CMND không được để trống")]*/
    [DisplayName("Số CCCD/CMND")]
    public string? SoCccd { get; set; }

    [DisplayName("Trạng thái")]
    public int? TrangThai { get; set; }

    public virtual HopDong? MaHopDongNavigation { get; set; }

    public virtual Lop? MaLopNavigation { get; set; }

    public virtual Phong? MaPhongNavigation { get; set; }

    public virtual Que? MaQueNavigation { get; set; }
    /*
        [Required(ErrorMessage = "Bạn phải chọn ảnh")]*/
    [NotMapped]
    public IFormFile? Image { get; set; }
}
