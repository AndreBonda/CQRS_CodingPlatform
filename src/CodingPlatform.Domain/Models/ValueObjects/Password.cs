using System.Text.RegularExpressions;
using CodingPlatform.Domain.Interfaces.Services;

namespace CodingPlatform.Domain.Models.ValueObjects;

public class Password
{
    public const int MIN_LENGTH = 6;
    public const string VALUE_REGEX = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$";
    public byte[] PasswordSalt { get; private set; }
    public byte[] PasswordHash { get; private set; }
    private readonly IPasswordHasingProvider _hashingProvider;

    public Password(string plainTextPassword, IPasswordHasingProvider hashingProvider)
    {
        if (!Regex.IsMatch(plainTextPassword, VALUE_REGEX) || plainTextPassword.Length < MIN_LENGTH)
            throw new ArgumentException(nameof(plainTextPassword));

        if (hashingProvider == null)
            throw new ArgumentException(nameof(hashingProvider));

        _hashingProvider = hashingProvider;

        HashPassword(plainTextPassword);
    }

    public Password(byte[] passwordSalt, byte[] passwordHash, IPasswordHasingProvider hashingProvider)
    {
        if (passwordSalt == null || passwordSalt.Length == 0)
            throw new ArgumentException(nameof(passwordSalt));

        if (passwordHash == null || passwordHash.Length == 0)
            throw new ArgumentException(nameof(passwordHash));

        if (hashingProvider == null)
            throw new ArgumentException(nameof(hashingProvider));

        _hashingProvider = hashingProvider;
    }

    public bool VerifyPassword(string plainTextPassword) => _hashingProvider.VerifyPassword(plainTextPassword, PasswordSalt, PasswordHash);

    private void HashPassword(string plainTextPassword)
    {
        var hashingResult = _hashingProvider.HashPassword(plainTextPassword);
        PasswordSalt = hashingResult.Salt;
        PasswordHash = hashingResult.Hash;
    }
}