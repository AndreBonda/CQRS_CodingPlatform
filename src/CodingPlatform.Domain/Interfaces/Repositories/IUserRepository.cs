using CodingPlatform.Domain.Models;

namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetByEmailAsync(string email);
}