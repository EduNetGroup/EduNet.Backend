using System.ComponentModel.DataAnnotations;
using EduNet.Backend.Service.Commons.Validators;

namespace EduNet.Backend.Service.Commons.Attributes;

public class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null) return new ValidationResult("Password can not be null");
        else
        {
            string password = value.ToString();
            if (password.Length < 8)
                return new ValidationResult("Password must be 8 or more characters");
            if (password.Length > 30)
                return new ValidationResult("Password must be less than 30 characters");
            var result = PasswordValidator.IsStrong(password);

            if (result.IsValid is false) return new ValidationResult(result.Message);
            return ValidationResult.Success;
        }
    }
}
