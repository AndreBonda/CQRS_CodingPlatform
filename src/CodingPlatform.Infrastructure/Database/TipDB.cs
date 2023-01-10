using System.ComponentModel.DataAnnotations;

namespace CodingPlatform.Infrastructure.Database;

public class TipDB
{
    [Key] public string Id { get; set; }
    [Required] public required DateTime CreateDate { get; set; }
    [Required] public required DateTime UpdateDate { get; set; }
    [Required] public required string Description { get; set; }
    [Required] public required int Order { get; set; }
}