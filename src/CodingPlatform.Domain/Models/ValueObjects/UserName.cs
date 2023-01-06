using System.Text.RegularExpressions;

namespace CodingPlatform.Domain.Models.ValueObjects;

public class Username
{
    public const int MIN_LENGTH = 3;
    public const string VALUE_REGEX = @"^[A-Za-z0-9]*$";

    public readonly string UsernameValue;

    public Username(string username)
    {
        if (!Regex.IsMatch(username, VALUE_REGEX))
            throw new ArgumentException(nameof(username));

        if (username.Length < MIN_LENGTH)
            throw new ArgumentException(nameof(username));

        UsernameValue = username;
    }
}