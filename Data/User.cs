using System.ComponentModel.DataAnnotations;

namespace BimaTech.Parking.Data;

public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public string Role { get; set; } = "Administrateur";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
