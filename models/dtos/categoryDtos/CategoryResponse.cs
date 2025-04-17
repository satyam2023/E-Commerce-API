using System.ComponentModel.DataAnnotations;
using ECommerce.Constants.LocalString;

public record CategoryDetails
{
    [Required]
    public required int CategoryId { get; set; }

    [Required(ErrorMessage = LocalString.categoryNameRequired)]
    [StringLength(100, ErrorMessage = LocalString.categoryNameLength)]
    public required string CategoryName { get; set; }

    [Required(ErrorMessage = LocalString.imageUrlRequired)]
    [Url(ErrorMessage = LocalString.invalidUrlFormat)]
    public required string ImageUrl { get; set; }

    [Required(ErrorMessage = LocalString.descriptionRequired)]
    [StringLength(500, ErrorMessage = LocalString.descriptionLength)]
    public required string Description { get; set; }
}
