using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MaktabBlog.Framework.Presentation.Utilities;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class IsValidNationalIdAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        ErrorMessage = "National is not valid";
        if (value is not string)
        {
            return false;
        }

        var stringifiedValue = value.ToString();

        if (string.IsNullOrWhiteSpace(stringifiedValue))
            return false;
        
        var regex = new Regex(@"^\d{10}$");
        return regex.IsMatch(stringifiedValue);
    }
}