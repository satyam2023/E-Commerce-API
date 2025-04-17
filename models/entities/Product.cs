using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    [Required]
    public required int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public required double Price { get; set; }

    [Required(ErrorMessage = "Available stock is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Available stock cannot be negative.")]
    public required int AvlStock { get; set; }

    [Required(ErrorMessage = "At least one product image is required.")]
    public required List<string> ProductImages { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public required string Description { get; set; }

    [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
    public required float Discount { get; set; } = 0;

    [Required(ErrorMessage = "CreatedBy is required.")]
    [StringLength(50, ErrorMessage = "CreatedBy cannot exceed 50 characters.")]
    public required string CreatedBy { get; set; }

    [Required(ErrorMessage = "CategoryId is required.")]
    public required int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public required Category Category { get; set; }
}