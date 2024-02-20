namespace EduNet.Backend.Service.Commons.Validators;

public class PasswordValidator
{
    public static (bool IsValid, string Message) IsStrong(string password)
    {
        bool isDigit = password.Any(x => char.IsDigit(x));
        if (!isDigit)
            return (false, "Password must contain at least 1 digit number");
        bool isUppercase = password.Any(x => char.IsUpper(x));
        if (!isUppercase)
            return (false, "Password must contain at least 1 uppercase character");

        return (true, "Valid Password");
    }
}
