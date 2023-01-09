using System.Text.RegularExpressions;

namespace CodingPlatform.Domain.Models.ValueObjects;

public class Email
{
    public const string VALUE_REGEX = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    public virtual string EmailValue { get; private set; }

    protected Email() { }

    public Email(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, VALUE_REGEX))
            throw new ArgumentException(nameof(email));

        EmailValue = email;
    }
}