using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Queries;

public record GetChallengeByAdmin(Guid Id, Guid CurrentUserId) : IRequest<ChallengeAdminVM>;