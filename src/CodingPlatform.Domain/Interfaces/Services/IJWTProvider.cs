namespace CodingPlatform.Domain.Interfaces.Services;

public interface IJWTProvider
{
    string GenerateJWT(Guid userId, string email, string keyGen);
}