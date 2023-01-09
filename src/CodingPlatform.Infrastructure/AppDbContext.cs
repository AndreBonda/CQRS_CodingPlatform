using CodingPlatform.Domain.Models;
using CodingPlatform.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CodingPlatform.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ChallengeDB> Challenges { get; set; }
    public DbSet<UserDB> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChallengeDB>()
            .HasOne<UserDB>()
            .WithOne()
            .HasForeignKey<ChallengeDB>(c => c.AdminId);
    }
}