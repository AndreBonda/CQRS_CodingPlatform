using System.ComponentModel.DataAnnotations;
using CodingPlatform.Domain.Models;

namespace CodingPlatform.Web.DTO.Challenges;

public class CreateChallengeDto
{
    [Required]
    [MinLength(1)]
    public string Title { get; set; }
    [Required]
    [MinLength(1)]
    public string Description { get; set; }
    [Required]
    [Range(Challenge._MIN_HOURS_DURATION_CHALLENGE, Challenge._MAX_HOURS_DURATION_CHALLENGE)]
    public int DurationInHours { get; set; }
    public IEnumerable<string> Tips { get; set; }
}