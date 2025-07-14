using System.ComponentModel.DataAnnotations;
using ECommerce.Constants.LocalString;
using ECommerce.Helper.Regex;

namespace ECommerce.Models.Dtos.User
{
    public class UserResponse
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = LocalString.nameValidationMsg)]
        public required string Name { get; set; }

        [Required(ErrorMessage = LocalString.emailRequired)]
        [RegularExpression(RegexExp.EMAIL_REGEX, ErrorMessage = LocalString.invalidEmailFormat)]
        public required string Email { get; set; }

        [Required(ErrorMessage = LocalString.phoneRequired)]
        [RegularExpression(RegexExp.PHONE_REGEX, ErrorMessage = LocalString.invalidPhoneFormat)]
        public required string PhoneNumber { get; set; }

        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
