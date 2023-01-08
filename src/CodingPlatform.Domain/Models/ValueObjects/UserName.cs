using System.Text.RegularExpressions;

namespace CodingPlatform.Domain.Models.ValueObjects;

public class UsernameTests
{
    public const int MIN_LENGTH = 3;
    public const string VALUE_REGEX = @"^[A-Za-z0-9]*$";

    public readonly string UsernameValue;

    public UsernameTests(string username)
    {
        if (string.IsNullOrWhiteSpace(username) ||
            username.Length < MIN_LENGTH ||
            !Regex.IsMatch(username, VALUE_REGEX))
            throw new ArgumentException(nameof(username));

        UsernameValue = username;
    }
}