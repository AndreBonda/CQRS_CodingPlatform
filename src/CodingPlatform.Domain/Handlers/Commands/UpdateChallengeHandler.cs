using CodingPlatform.Domain.Commands;
using CodingPlatform.Domain.Exception;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Models;
using MediatR;

namespace CodingPlatform.Domain.Handlers.Commands;

public class UpdateChallengeHandler : IRequestHandler<UpdateChallengeCmd, Unit>
{
    private readonly IChallengeRepository _challengeRepository;

    public UpdateChallengeHandler(IChallengeRepository challengeRepository)
    {
        _challengeRepository = challengeRepository;
    }

    public async Task<Unit> Handle(UpdateChallengeCmd request, CancellationToken cancellationToken)
    {
        var challenge = await _challengeRepository.GetByIdAsync(request.Id);

        if (challenge == null) throw new NotFoundException("Challenge not found.");

        challenge.UpdateChallenge(request.CurrentUserId, request.Title, request.Description, request.EndDate, request.Tips);
        await _challengeRepository.UpdateAsync(challenge);
        await _challengeRepository.SaveAsync();

        return Unit.Value;
    }
}