using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Models;
using CodingPlatform.Domain.ViewModels.Challenges;

public class ChallengeRepository : IChallengeRepository
{
    #region Command
    public Task AddAsync(Challenge challenge)
    {
        
        throw new NotImplementedException();
    }

    public Task<Challenge> GetBydIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Query
    public Task<ChallengeVM> GetChallengeVMByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    #endregion
}