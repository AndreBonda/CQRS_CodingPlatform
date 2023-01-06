using System.ComponentModel.DataAnnotations;
using CodingPlatform.Web.Global;

namespace CodingPlatform.Web.DTO;

public class UserDto
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateTime DateCreated { get; set; }
}

public class LoginUserDto
{
    [Required]
    [RegularExpression(Domain.Models.ValueObjects.Email.VALUE_REGEX, ErrorMessage = "Email format not valid")]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    [RegularExpression(Consts.PasswordRegex, ErrorMessage = Consts.PasswordFormatError)]
    public string Password { get; set; }
}

public class RegisterUserDto : LoginUserDto
{
    [Required]
    [MinLength(CodingPlatform.Domain.Models.ValueObjects.Username.MIN_LENGTH)]
    [RegularExpression(CodingPlatform.Domain.Models.ValueObjects.Username.VALUE_REGEX, ErrorMessage = "Username must be an alphanumeric")]
    public string Username { get; set; }
}