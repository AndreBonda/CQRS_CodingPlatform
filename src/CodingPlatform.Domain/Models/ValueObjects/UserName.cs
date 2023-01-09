using System.Text.RegularExpressions;

namespace CodingPlatform.Domain.Models.ValueObjects;

public class Username
{
    public const int MIN_LENGTH = 3;
    public const string VALUE_REGEX = @"^[A-Za-z0-9]*$";
    public virtual string UsernameValue { get; private set; }

    protected Username() { }

    public Username(string username)
    {
        if (string.IsNullOrWhiteSpace(username) ||
            username.Length < MIN_LENGTH ||
            !Regex.IsMatch(username, VALUE_REGEX))
            throw new ArgumentException(nameof(username));

        UsernameValue = username;
    }
}