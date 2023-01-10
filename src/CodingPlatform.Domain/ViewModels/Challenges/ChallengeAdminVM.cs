using CodingPlatform.Domain.ViewModels.Tips;

namespace CodingPlatform.Domain.ViewModels.Challenges;

public record ChallengeAdminVM(string Title, string Description, DateTime StartDateTime, DateTime EndDateTime, DateTime UpdateDateTime, IEnumerable<TipVM> Tips);