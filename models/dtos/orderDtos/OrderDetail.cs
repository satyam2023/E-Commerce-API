using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Constants.LocalString;

public record OrderDetail
{
    public required DateTime DeliveryDate { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = LocalString.totalAmtMoreThanZero)]
    public decimal TotalAmount { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }
    public required ICollection<OrderItem> OrderItems { get; set; }
}
