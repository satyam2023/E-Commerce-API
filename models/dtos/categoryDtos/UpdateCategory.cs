using System.ComponentModel.DataAnnotations;
using ECommerce.Constants.LocalString;

[AtLeastOneRequired(nameof(CategoryName), nameof(ImageUrl), nameof(Description))]
public record UpdateCategoryDetail
{
    [StringLength(100, ErrorMessage = LocalString.categoryNameLength)]
    public string? CategoryName { get; set; }

    [Url(ErrorMessage = LocalString.invalidUrlFormat)]
    public string? ImageUrl { get; set; }

    [StringLength(500, ErrorMessage = LocalString.descriptionLength)]
    public string? Description { get; set; }
}
