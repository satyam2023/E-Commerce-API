using System.ComponentModel.DataAnnotations;

public record DeleteUserRequest
{
    [Required]
    public required int userId { get; set; }
}
