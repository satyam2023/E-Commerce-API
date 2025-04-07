using System.ComponentModel.DataAnnotations;
using ECommerce.Constants.LocalString;
using ECommerce.Helper.Regex;

namespace ECommerce.Models.Dtos.User
{
    [AtLeastOneRequired(nameof(Name), nameof(PhoneNumber))]
    public record UpdateUser
    {
        [StringLength(50, MinimumLength = 4, ErrorMessage = LocalString.nameValidationMsg)]
        public string? Name { get; set; }

        [RegularExpression(RegexExp.PHONE_REGEX, ErrorMessage = LocalString.invalidPhoneFormat)]
        public string? PhoneNumber { get; set; }
    }
}
