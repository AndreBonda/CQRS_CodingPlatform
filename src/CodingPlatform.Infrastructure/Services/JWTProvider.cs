using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodingPlatform.Domain.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace CodingPlatform.Infrastructure.Services;

public class JWTProvider : IJWTProvider
{
    private const int _JWT_EXPIRATION_IN_DAYS = 7;

    public string GenerateJWT(Guid userId, string email, string keyGen)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyGen));
        var cred = new SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: new List<Claim>
            {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            },
            expires: DateTime.UtcNow.AddDays(_JWT_EXPIRATION_IN_DAYS),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}