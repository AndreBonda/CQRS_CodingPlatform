using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Commands;

public record CreateChallengeCmd(Guid Id, Guid AdminId, string Title, string Description, DateTime EndDate, IEnumerable<string> Tips) : IRequest;