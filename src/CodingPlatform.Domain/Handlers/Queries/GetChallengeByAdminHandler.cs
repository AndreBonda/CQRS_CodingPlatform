using CodingPlatform.Domain.Exception;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Domain.Queries;
using CodingPlatform.Domain.ViewModels.Challenges;
using CodingPlatform.Domain.ViewModels.Tips;
using MediatR;

namespace CodingPlatform.Domain.Handlers.Queries;

public class GetChallengeByAdminHandler : IRequestHandler<GetChallengeByAdmin, ChallengeAdminVM>
{
    private readonly IChallengeRepository _challengeRepository;

    public GetChallengeByAdminHandler(IChallengeRepository challengeRepository)
    {
        _challengeRepository = challengeRepository;
    }

    // TODO: situazione "ibrida" in cui ho necessità dell'oggetto complesso per eseguire logiche per una visualizzazione.
    // Come poterle gestire al meglio?
    public async Task<ChallengeAdminVM> Handle(GetChallengeByAdmin request, CancellationToken cancellationToken)
    {
        var challenge = await _challengeRepository.GetByIdAsync(request.Id);

        if (challenge == null) throw new NotFoundException("Challenge not found");

        if (!challenge.IsAdmin(request.CurrentUserId)) throw new ForbiddenException("User is not the admin of the challenge");

        return new ChallengeAdminVM(
            challenge.Title,
            challenge.Description,
            challenge.CreateDate,
            challenge.EndDate,
            challenge.UpdateDate,
            challenge.Tips.Select(t => new TipVM(t.Description, t.Order))
        );
    }
}