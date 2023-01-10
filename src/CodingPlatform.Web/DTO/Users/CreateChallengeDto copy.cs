using System.ComponentModel.DataAnnotations;

namespace CodingPlatform.Web.DTO.Challenges;

public class SignInDto
{
    [Required]
    [RegularExpression(CodingPlatform.Domain.Models.ValueObjects.Email.VALUE_REGEX)]
    public string Email { get; set; }
    [Required]
    [MinLength(CodingPlatform.Domain.Models.ValueObjects.Password.MIN_LENGTH)]
    [RegularExpression(CodingPlatform.Domain.Models.ValueObjects.Password.VALUE_REGEX)]
    public string Password { get; set; }
}