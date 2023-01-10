using CodingPlatform.Domain.Exception;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Domain.Queries;
using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Handlers.Queries;

public class GetJWTHandler : IRequestHandler<GetJWT, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJWTProvider _JWTProvider;

    public GetJWTHandler(IUserRepository userRepository, IJWTProvider jwtProvider)
    {
        _userRepository = userRepository;
        _JWTProvider = jwtProvider;
    }

    public async Task<string> Handle(GetJWT request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null) throw new NotFoundException("User not found");
        if (!user.IsPasswordCorrect(request.PlainTextPassword)) throw new ForbiddenException("Wrong password");

        return _JWTProvider.GenerateJWT(user.Id, request.Email, request.KeyGen);
    }
}