using CodingPlatform.Domain.Models.ValueObjects;

namespace CodingPlatform.Domain.Models;

public class User : BaseEntity
{
    private Email _email;
    private UsernameTests _username;
    private Password _password;

    public string UserName => _username.UsernameValue;
    public string Email => _email.EmailValue;
    public byte[] PasswordSalt => _password.PasswordSalt;
    public byte[] PasswordHash => _password.PasswordHash;

    public User(Guid id, Email email, UsernameTests username, Password password) : base(id)
    {
        if (email == null) throw new ArgumentException(nameof(email));

        if (username == null) throw new ArgumentException(nameof(username));

        if (password == null) throw new ArgumentException(nameof(password));

        _email = email;
        _username = username;
        _password = password;
    }
}