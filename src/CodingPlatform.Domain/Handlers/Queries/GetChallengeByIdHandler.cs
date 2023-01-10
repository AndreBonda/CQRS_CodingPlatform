using CodingPlatform.Domain.Exception;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Queries;
using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Handlers.Queries;

public class GetChallengeByIdHandler : IRequestHandler<GetChallengeById, ChallengeVM>
{
    private readonly IChallengeRepository _challengeRepository;

    public GetChallengeByIdHandler(IChallengeRepository challengeRepository)
    {
        _challengeRepository = challengeRepository;
    }

    public async Task<ChallengeVM> Handle(GetChallengeById request, CancellationToken cancellationToken)
    {
        var challenge = await _challengeRepository.GetChallengeVMByIdAsync(request.Id);

        if (challenge == null) throw new NotFoundException("Challenge not found");

        return challenge;
    }
}