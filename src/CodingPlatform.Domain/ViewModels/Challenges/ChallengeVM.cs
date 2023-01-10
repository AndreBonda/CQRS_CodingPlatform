namespace CodingPlatform.Domain.ViewModels.Challenges;

public record ChallengeVM(string Title, bool HasTips, DateTime StartDateTime, DateTime EndDateTime);