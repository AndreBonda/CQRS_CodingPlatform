namespace CodingPlatform.Web.Global;

public static class Consts
{
    public const string PasswordRegex = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$";
    public const string PasswordFormatError =
        "Password must contain an uppercase letter, a lowercase letter, a number and a special character";
    public const string JwtConfigSections = "JWT:Key";
}