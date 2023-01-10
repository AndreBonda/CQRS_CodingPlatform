using CodingPlatform.Domain.ViewModels.Challenges;
using MediatR;

namespace CodingPlatform.Domain.Commands;

public record CreateUserCmd(string Email, string Username, string Password) : IRequest;