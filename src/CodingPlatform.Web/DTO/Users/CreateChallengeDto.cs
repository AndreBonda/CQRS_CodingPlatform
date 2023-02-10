using System.ComponentModel.DataAnnotations;

namespace CodingPlatform.Web.DTO.Challenges;

public class SignUpDto
{
    [Required]
    [RegularExpression(CodingPlatform.Domain.Models.Email.VALUE_REGEX)]
    public string Email { get; set; }
    [Required]
    [MinLength(CodingPlatform.Domain.Models.Username.MIN_LENGTH)]
    [RegularExpression(CodingPlatform.Domain.Models.Username.VALUE_REGEX)]
    public string Username { get; set; }
    [Required]
    [MinLength(CodingPlatform.Domain.Models.Password.MIN_LENGTH)]
    [RegularExpression(CodingPlatform.Domain.Models.Password.VALUE_REGEX)]
    public string Password { get; set; }
}