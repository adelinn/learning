using Microsoft.EntityFrameworkCore;
using dotNetLearn.Models;

namespace dotNetLearn.Services;

public class RandDBContext : DbContext
{
    public DbSet<RandRecord> RandRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host="+(Environment.GetEnvironmentVariable("DB_HOST")??"host.docker.internal")+";Port=5435;Username=root;Password=root;Database=test_db");
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Timestamp).HasDefaultValueSql("now()").HasColumnName("timestamp");
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Numbers).HasColumnName("numbers");
    // }
}