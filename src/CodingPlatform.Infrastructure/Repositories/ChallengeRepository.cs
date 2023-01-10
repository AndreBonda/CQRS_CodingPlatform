using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain.Models;
using CodingPlatform.Domain.ViewModels.Challenges;
using CodingPlatform.Infrastructure;
using CodingPlatform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CodingPlatform.Infrastructure.Repositories;

public class ChallengeRepository : IChallengeRepository
{
    private readonly AppDbContext _dbCtx;

    public ChallengeRepository(AppDbContext dbCtx)
    {
        _dbCtx = dbCtx;
    }

    public async Task AddAsync(Challenge challenge)
    {
        var challengeDb = new ChallengeDB
        {
            Id = challenge.Id.ToString(),
            CreateDate = challenge.CreateDate,
            UpdateDate = challenge.UpdateDate,
            EndDate = challenge.EndDate,
            Title = challenge.Title,
            Description = challenge.Description,
            AdminId = challenge.AdminId.ToString(),
            Tips = challenge.Tips?.Select(t => new TipDB
            {
                Id = Guid.NewGuid().ToString(),
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                Description = t.Description,
                Order = t.Order
            }).ToList()
        };

        await _dbCtx.AddAsync(challengeDb);
        await _dbCtx.SaveChangesAsync();
    }

    public async Task<Challenge> GetByIdAsync(Guid id)
    {
        var challengeDb = await _dbCtx
            .Challenges
            .Include(c => c.Tips)
            .FirstOrDefaultAsync(c => c.Id == id.ToString());

        if (challengeDb == null) return null;

        return new Challenge(
            Guid.Parse(challengeDb.Id),
            Guid.Parse(challengeDb.AdminId),
            challengeDb.Title,
            challengeDb.Description,
            challengeDb.EndDate,
            challengeDb.CreateDate,
            challengeDb.UpdateDate,
            challengeDb.Tips.Select(t => new Tip(
                Guid.Parse(t.Id),
                t.Description,
                t.Order,
                t.CreateDate,
                t.UpdateDate
            )));
    }

    public async Task UpdateAsync(Challenge challenge)
    {
        var challengeDb = await _dbCtx
            .Challenges
            .Include(c => c.Tips)
            .FirstAsync(c => c.Id == challenge.Id.ToString());

        challengeDb.Title = challenge.Title;
        challengeDb.Description = challenge.Description;
        challengeDb.EndDate = challenge.EndDate;

        _dbCtx.Tips.RemoveRange(challengeDb.Tips);
        challengeDb.Tips = challenge.Tips.Select(t => new TipDB
        {
            Id = t.Id.ToString(),
            CreateDate = t.CreateDate,
            Description = t.Description,
            Order = t.Order,
            UpdateDate = t.UpdateDate
        }).ToList();
        challengeDb.UpdateDate = challenge.UpdateDate;
    }

    public async Task SaveAsync() => await _dbCtx.SaveChangesAsync();

    #region ReadModels
    public async Task<ChallengeVM> GetChallengeVMByIdAsync(Guid id)
    {
        var challengeDb = await _dbCtx
        .Challenges
        .Include(c => c.Tips)
        .FirstOrDefaultAsync(c => c.Id == id.ToString());

        if (challengeDb == null) return null;

        return new ChallengeVM(
            challengeDb.Title,
            challengeDb.Tips.Any(),
            challengeDb.CreateDate,
            challengeDb.EndDate
        );
    }

    #endregion
}