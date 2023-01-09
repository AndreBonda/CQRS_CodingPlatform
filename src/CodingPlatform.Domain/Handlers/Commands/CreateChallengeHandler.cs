using CodingPlatform.Domain.Commands;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Models;
using MediatR;

namespace CodingPlatform.Domain.Handlers.Commands;

public class CreateChallengeHandler : IRequestHandler<CreateChallengeCmd, Unit>
{
    private readonly IChallengeRepository _challengeRepository;

    public CreateChallengeHandler(IChallengeRepository challengeRepository)
    {
        _challengeRepository = challengeRepository;
    }

    public async Task<Unit> Handle(CreateChallengeCmd request, CancellationToken cancellationToken)
    {
        await _challengeRepository.AddAsync(new Challenge(
            request.Id, request.AdminId, request.Title, request.Description, request.DurationInHours, request.Tips));
        await _challengeRepository.SaveAsync();

        return Unit.Value;
    }
}