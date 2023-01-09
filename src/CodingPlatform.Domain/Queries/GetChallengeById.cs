using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Queries;

public record GetChallengeById(Guid Id) : IRequest<ChallengeVM>;