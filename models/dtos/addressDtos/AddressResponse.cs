using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Constants.LocalString;

public record AddressResponse
{
    [Required(ErrorMessage = LocalString.addressLine1Required)]
    [StringLength(100, ErrorMessage = LocalString.addressLine1Length)]
    public required string AddressLine1 { get; set; }

    [StringLength(100, ErrorMessage = LocalString.addressLine2Length)]
    public string? AddressLine2 { get; set; }

    [Required(ErrorMessage = LocalString.cityRequired)]
    [StringLength(50, ErrorMessage = LocalString.cityLength)]
    public required string City { get; set; }

    [Required(ErrorMessage = LocalString.stateRequired)]
    [StringLength(50, ErrorMessage = LocalString.stateLength)]
    public required string State { get; set; }

    [Required(ErrorMessage = LocalString.postalCodeRequired)]
    public required string PostalCode { get; set; }

    [Required(ErrorMessage = LocalString.countryRequired)]
    [StringLength(50, ErrorMessage = LocalString.countryLength)]
    public required string Country { get; set; }

    [Required]
    public required bool IsPrimary { get; set; }
}
