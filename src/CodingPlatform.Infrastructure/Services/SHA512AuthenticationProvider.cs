using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CodingPlatform.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace CodingPlatform.Infrastructure.Services;

public class SHA512AuthenticationProvider : IPasswordHasingProvider
{
    public SHA512AuthenticationProvider()
    {
    }

    public (byte[] Salt, byte[] Hash) HashPassword(string plainTextPassword)
    {
        if (string.IsNullOrEmpty(plainTextPassword)) throw new ArgumentNullException(nameof(plainTextPassword));

        using var hmac = new HMACSHA512();

        return new(
            hmac.Key,
            hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainTextPassword)));
    }

    public bool VerifyPassword(string plainTextPassword, byte[] salt, byte[] hashPassword)
    {
        using var hmac = new HMACSHA512(salt);
        return hashPassword.SequenceEqual(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(plainTextPassword)));
    }

    public string GenerateJWT(long userId, string email, string keyGen)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyGen));
        var cred = new SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: new List<Claim>
            {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            },
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}