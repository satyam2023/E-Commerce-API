using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record CreateOrder
{
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public required DateTime OrderDate { get; set; }

    public required DateTime DeliveryDate { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0")]
    public decimal TotalAmount { get; set; }

    [StringLength(255)]
    public string? Notes { get; set; }
    public required ICollection<CreateOrderItem> OrderItems { get; set; }
}
