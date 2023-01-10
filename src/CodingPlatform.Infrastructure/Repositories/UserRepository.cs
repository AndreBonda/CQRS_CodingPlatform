using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Interfaces.Services;
using CodingPlatform.Domain.Models;
using CodingPlatform.Domain.Models.ValueObjects;
using CodingPlatform.Infrastructure;
using CodingPlatform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CodingPlatform.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbCtx;
    private IPasswordHasingProvider _passwordProvider;

    public UserRepository(AppDbContext dbCtx, IPasswordHasingProvider passwordProvider)
    {
        _dbCtx = dbCtx;
        _passwordProvider = passwordProvider;
    }

    public async Task AddAsync(User user)
    {
        var userDb = new UserDB
        {
            Id = user.Id.ToString(),
            CreateDate = user.CreateDate,
            UpdateDate = user.UpdateDate,
            Email = user.Email,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt
        };

        await _dbCtx.AddAsync(userDb);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var userDb = await _dbCtx
            .Users
            .FirstOrDefaultAsync(u => u.Id == id.ToString());

        if (userDb == null) return null;

        return new User(
            Guid.Parse(userDb.Id),
            new Email(userDb.Email),
            new Username(userDb.Username),
            new Password(userDb.PasswordSalt, userDb.PasswordHash, _passwordProvider),
            userDb.CreateDate,
            userDb.UpdateDate
        );
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var userDb = await _dbCtx
            .Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (userDb == null) return null;

        return new User(
            Guid.Parse(userDb.Id),
            new Email(userDb.Email),
            new Username(userDb.Username),
            new Password(userDb.PasswordSalt, userDb.PasswordHash, _passwordProvider),
            userDb.CreateDate,
            userDb.UpdateDate
        );
    }

    public async Task SaveAsync() => await _dbCtx.SaveChangesAsync();
}