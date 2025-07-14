using System.ComponentModel.DataAnnotations;
using ECommerce.Constants.LocalString;
using ECommerce.Helper.Regex;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 4, ErrorMessage = LocalString.nameValidationMsg)]
    public required string Name { get; set; }

    [Required(ErrorMessage = LocalString.emailRequired)]
    [RegularExpression(RegexExp.EMAIL_REGEX, ErrorMessage = LocalString.invalidEmailFormat)]
    public required string Email { get; set; }

    [Required(ErrorMessage = LocalString.phoneRequired)]
    [RegularExpression(RegexExp.PHONE_REGEX, ErrorMessage = LocalString.invalidPhoneFormat)]
    public required string PhoneNumber { get; set; }

    [Required(ErrorMessage = LocalString.passwordRequired)]
    [RegularExpression(RegexExp.PASSWORD_REGEX, ErrorMessage = LocalString.passwordErrorMsg)]
    public required string Password { get; set; }

    [Required]
    public UserRole Role { get; set; }

    public required string RefreshToken { get; set; }

    public ICollection<Address>? Addresses { get; set; }
}
