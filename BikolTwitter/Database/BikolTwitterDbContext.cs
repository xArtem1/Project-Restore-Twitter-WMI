using BikolTwitter.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikolTwitter.Database;

public class BikolTwitterDbContext : DbContext
{
    public DbSet<BikolSub> BikolSubs { get; set; }

	public BikolTwitterDbContext(DbContextOptions options)
		: base(options)
	{

	}

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
    }
}
