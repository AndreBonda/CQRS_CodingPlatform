using System.ComponentModel.DataAnnotations;

namespace CodingPlatform.Infrastructure.Database;

public class ChallengeDB
{
    [Key] public string Id { get; set; }
    [Required] public required DateTime CreateDate { get; set; }
    [Required] public required DateTime UpdateDate { get; set; }
    [Required] public required string Title { get; set; }
    [Required] public required string Description { get; set; }
    // // public IEnumerable<TipDB> Tips { get; set; }
    [Required] public required string AdminId { get; set; }

}