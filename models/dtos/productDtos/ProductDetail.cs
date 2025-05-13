using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Constants.LocalString;

public record ProductDetail
{
    [Key]
    [Required]
    public required int ProductId { get; set; }

    [Required(ErrorMessage = LocalString.productNameReq)]
    [StringLength(100, ErrorMessage = LocalString.productNameErrorMsg)]
    public required string Name { get; set; }

    [Required(ErrorMessage = LocalString.productPriceReq)]
    [Range(0.01, double.MaxValue, ErrorMessage = LocalString.priceErrors)]
    public required double Price { get; set; }

    [Required(ErrorMessage = LocalString.avlStockReq)]
    [Range(0, int.MaxValue, ErrorMessage = LocalString.avlStockMsg)]
    public required int AvlStock { get; set; }

    [Required(ErrorMessage = LocalString.productImgMsg)]
    public required List<string> ProductImages { get; set; }

    [Required(ErrorMessage = LocalString.productDescReq)]
    [StringLength(500, ErrorMessage = LocalString.productDescMsg)]
    public required string Description { get; set; }

    [Range(0, 100, ErrorMessage = LocalString.productDiscountError)]
    public required float Discount { get; set; } = 0;

    [Required(ErrorMessage = LocalString.createdByReq)]
    [StringLength(50, ErrorMessage = LocalString.createdByErrorMsg)]
    public required string CreatedBy { get; set; }

    [Required(ErrorMessage = LocalString.categoryIdReq)]
    public required int CategoryId { get; set; }
}
