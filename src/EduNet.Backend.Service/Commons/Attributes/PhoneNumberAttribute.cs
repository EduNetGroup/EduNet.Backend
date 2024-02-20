using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace EduNet.Backend.Service.Commons.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string phoneNumber = (string)value;
        if (String.IsNullOrWhiteSpace(phoneNumber))
        {
            return new ValidationResult("Please, Enter the correct phone number!");
        }
        else
        {
            Regex regex = new Regex("^(?:\\+998([- ])?(90|91|93|94|95|98|99|33|50|77|97|71|99|88)([- ])?(\\d{3})([- ])?(\\d{2})([- ])?(\\d{2}))");

            return (regex.Match(phoneNumber).Success) ? ValidationResult.Success
                : new ValidationResult("Please, Enter the correct phone number!");
        }
    }
}
