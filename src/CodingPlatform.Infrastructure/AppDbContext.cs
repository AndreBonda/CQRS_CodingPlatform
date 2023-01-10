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
    public DbSet<TipDB> Tips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // one-to-one relationship without navigation property. FK in challenge table
        modelBuilder.Entity<ChallengeDB>()
            .HasOne<UserDB>()
            .WithMany()
            .HasForeignKey(c => c.AdminId);
    }
}