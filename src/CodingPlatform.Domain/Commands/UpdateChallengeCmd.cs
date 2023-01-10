using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Commands;

public record UpdateChallengeCmd(Guid Id, Guid CurrentUserId, string Title, string Description, DateTime EndDate, IEnumerable<string> Tips) : IRequest;