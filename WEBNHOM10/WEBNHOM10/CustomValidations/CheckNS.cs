using System.ComponentModel.DataAnnotations;

namespace WEBNHOM10.CustomValidations
{
    public class CheckNS:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult(this.ErrorMessage);
            }
            var ns = DateTime.Parse(value.ToString());
            var age = DateTime.Now.Year - ns.Year;
            if (age >= 18)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}
