
using System.ComponentModel.DataAnnotations;
using WEBNHOM10.Models;

namespace WEBNHOM10.CustomValidations
{
    public class CheckTK:ValidationAttribute
    {
        QlKtxContext db = new QlKtxContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult(this.ErrorMessage);

            }
            string username = value.ToString();
            if (db.SinhViens.FirstOrDefault(x => x.TaiKhoan == username) ==null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}
