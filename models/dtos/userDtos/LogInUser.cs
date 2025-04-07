


using System.ComponentModel.DataAnnotations;
using ECommerce.Constants.LocalString;
using ECommerce.Helper.Regex;

public class LogInUserRequest
{
    [Required(ErrorMessage = LocalString.emailRequired)]
    [RegularExpression(RegexExp.EMAIL_REGEX,
     ErrorMessage = LocalString.invalidEmailFormat)]
    public required string Email { get; set; }

    [Required(ErrorMessage = LocalString.passwordRequired)]
    [RegularExpression(RegexExp.PASSWORD_REGEX, ErrorMessage = LocalString.passwordErrorMsg)]
    public required string Password { get; set; }

}