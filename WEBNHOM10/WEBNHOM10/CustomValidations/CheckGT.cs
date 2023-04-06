using System.ComponentModel.DataAnnotations;

namespace WEBNHOM10.CustomValidations
{
    public class CheckGT:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult(this.ErrorMessage);
            }
            string gt = (string)value;
            if(gt.Equals("Nam") || gt.Equals("Nữ") || gt.Equals("Khác"))
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
