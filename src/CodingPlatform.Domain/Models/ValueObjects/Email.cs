using System.Text.RegularExpressions;

namespace CodingPlatform.Domain.Models.ValueObjects;

public class Email
{
    public const string VALUE_REGEX = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    public readonly string EmailValue;

    public Email(string email)
    {
        if (!Regex.IsMatch(email, VALUE_REGEX))
            throw new ArgumentException(nameof(email));

        EmailValue = email;
    }
}