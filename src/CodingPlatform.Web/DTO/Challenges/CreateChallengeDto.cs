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
    public DateTime EndDate { get; set; }
    public IEnumerable<string> Tips { get; set; }
}