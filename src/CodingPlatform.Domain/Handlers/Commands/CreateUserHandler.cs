using CodingPlatform.Domain.Commands;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Domain.Models;
using MediatR;

namespace CodingPlatform.Domain.Handlers.Commands;

public class CreateUserHandler : IRequestHandler<CreateUserCmd, Unit>
{
    private readonly IUserRepository _userRepository;
    private IPasswordHasingProvider _passwordProvider;

    public CreateUserHandler(IUserRepository userRepository, IPasswordHasingProvider passwordProvider)
    {
        _userRepository = userRepository;
        _passwordProvider = passwordProvider;
    }

    public async Task<Unit> Handle(CreateUserCmd request, CancellationToken cancellationToken)
    {
        await _userRepository.AddAsync
        (
            // TODO: Referenziare qui Value Objects?
            new User(
                Guid.NewGuid(),
                new Email(request.Email),
                new Username(request.Username),
                new Password(request.Password, _passwordProvider)
            )
        );
        await _userRepository.SaveAsync();

        return Unit.Value;
    }
}