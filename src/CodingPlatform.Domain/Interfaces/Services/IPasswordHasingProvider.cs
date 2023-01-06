namespace CodingPlatform.Domain.Interfaces.Services;

public interface IPasswordHasingProvider
{
    (byte[] Salt, byte[] Hash) HashPassword(string plainTextPassword);
    bool VerifyPassword(string plainTextPassword, byte[] salt, byte[] hashPassword);
}