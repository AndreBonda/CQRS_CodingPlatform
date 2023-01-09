using CodingPlatform.Domain.Models;
using CodingPlatform.Domain.ViewModels.Challenges;

namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IChallengeRepository
{
    #region Command
    Task<Challenge> GetBydIdAsync(Guid id);
    Task AddAsync(Challenge challenge);
    Task SaveAsync();
    #endregion

    #region Query
    Task<ChallengeVM> GetChallengeVMByIdAsync(Guid id);
    #endregion
}