using System.ComponentModel.DataAnnotations;

namespace Logistics.Web.API.Attributes
{
    public class IdValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is int id))
                return new ValidationResult("Id must be a valid number greater than 0");

            if(id > 0)
                return ValidationResult.Success;

            return new ValidationResult("Id must be greater than 0");
        }
    }
}