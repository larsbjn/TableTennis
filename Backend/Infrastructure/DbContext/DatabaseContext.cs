using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext;

public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Rule> Rules { get; set; }

    public string DbPath { get; }

    public DatabaseContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"{Environment.GetEnvironmentVariable("ConnectionString")}",
            builder => builder.EnableRetryOnFailure());
}