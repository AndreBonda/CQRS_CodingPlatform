using CodingPlatform.Domain.Models;

namespace CodingPlatform.Domain.Models;

public class User : BaseEntity
{
    private Email _email;
    private Username _username;
    private Password _password;

    public string Username => _username.UsernameValue;
    public string Email => _email.EmailValue;
    public byte[] PasswordSalt => _password.PasswordSalt;
    public byte[] PasswordHash => _password.PasswordHash;

    public User(Guid id, Email email, Username username, Password password)
    : base(id, DateTime.UtcNow)
    {
        _email = email;
        _username = username;
        _password = password;

        Validate();
    }

    public User(Guid id, Email email, Username username, Password password, DateTime createDate, DateTime updateDate)
    : base(id)
    {
        _email = email;
        _username = username;
        _password = password;
        CreateDate = createDate;
        UpdateDate = updateDate;

        Validate();
    }

    public bool IsPasswordCorrect(string plainTextPassword) => _password.IsPasswordCorrect(plainTextPassword);

    protected override void Validate()
    {
        base.Validate();

        if (_email == null) throw new ArgumentNullException("Email can not be null");
        if (_username == null) throw new ArgumentNullException("Username can not be null");
        if (_password == null) throw new ArgumentNullException("Password can not be null");
    }
}