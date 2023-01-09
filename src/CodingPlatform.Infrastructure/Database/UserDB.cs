using System.ComponentModel.DataAnnotations;

namespace CodingPlatform.Infrastructure.Database;

public class UserDB
{
    [Key] public string Id { get; set; }
    [Required] public required DateTime CreateDate { get; set; }
    [Required] public required DateTime UpdateDate { get; set; }
    [Required] public required string Email { get; set; }
    [Required] public required string Username { get; set; }
    [Required] public required byte[] PasswordSalt { get; set; }
    [Required] public required byte[] PasswordHash { get; set; }
}