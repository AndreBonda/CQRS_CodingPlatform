using System.ComponentModel.DataAnnotations;

namespace CodingPlatform.Web.DTO.Challenges;

public class SignInDto
{
    [Required]
    [RegularExpression(CodingPlatform.Domain.Models.Email.VALUE_REGEX)]
    public string Email { get; set; }
    [Required]
    [MinLength(CodingPlatform.Domain.Models.Password.MIN_LENGTH)]
    [RegularExpression(CodingPlatform.Domain.Models.Password.VALUE_REGEX)]
    public string Password { get; set; }
}