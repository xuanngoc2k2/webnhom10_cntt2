using System.ComponentModel.DataAnnotations;
using WEBNHOM10.Models;

namespace WEBNHOM10.CustomValidations
{
    public class MSVduynhat:ValidationAttribute
    {
        QlKtxContext db = new QlKtxContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int masv = Convert.ToInt32(value);
            var sv = db.SinhViens.FirstOrDefault(x => x.MaSinhVien == masv);
            if(sv == null)
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
