using BikolTwitter.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikolTwitter.Database;

/// <inheritdoc/>
public class BikolTwitterDbContext : DbContext
{
    /// <summary>
    /// Represents BikolSubs table in database.
    /// </summary>
    public DbSet<BikolSub> BikolSubs { get; set; }
    /// <summary>
    /// Reptresents BikolPosts table in database.
    /// </summary>
    public DbSet<BikolPost> BikolPosts { get; set; }

    /// <inheritdoc/>
    public BikolTwitterDbContext(DbContextOptions options)
		: base(options)
	{

	}

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BikolSub>(e =>
        {
            e.Property(bs => bs.Username)
             .IsRequired();

            e.HasIndex(e => e.Username)
             .IsUnique();

            e.Property(e => e.ProfitSum)
             .HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<BikolPost>(e =>
        {
            e.HasIndex(p => p.TwitterId)
             .IsUnique();
            e.Property(p => p.Profit)
             .HasColumnType("decimal(5,2)");
        });
    }
}
