using CodingPlatform.Domain.Models;
using CodingPlatform.Domain.ViewModels.Challenges;

namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IChallengeRepository : IRepository<Challenge, Guid>
{
    Task<ChallengeVM> GetChallengeVMByIdAsync(Guid id);
}