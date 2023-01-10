using MediatR;

namespace CodingPlatform.Domain.Queries;

public record GetJWT(string Email, string PlainTextPassword, string KeyGen) : IRequest<string>;