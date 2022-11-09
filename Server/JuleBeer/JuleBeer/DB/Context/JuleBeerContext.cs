using Microsoft.EntityFrameworkCore;
using System.Reflection;
using JuleBeer.DB.Entities;

namespace JuleBeer.DB.Context;

public class JuleBeerContext : DbContext
{

    public static string ConnectionString { get; set; }

    public JuleBeerContext() { }

    public DbSet<User> Users { get; set; }
    public DbSet<Beer> Beers { get; set; }
    public DbSet<BeerReview> BeerReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder == null)
        {
            throw new ArgumentNullException(nameof(optionsBuilder));
        }

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
