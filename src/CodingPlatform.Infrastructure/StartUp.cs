using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Infrastructure.Repositories;
using CodingPlatform.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodingPlatform.Infrastructure;

public static class StartUp
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IChallengeRepository, ChallengeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<IPasswordHasingProvider, SHA512PasswordHashingProvider>();
        services.AddSingleton<IJWTProvider, SHA512JWTProvider>();

        return services;
    }
}