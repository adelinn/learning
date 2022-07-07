using Microsoft.EntityFrameworkCore;
using dotNetLearn.Models;

namespace dotNetLearn.Services;

public class RandDBContext : DbContext
{
    public DbSet<RandRecord> RandRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string host = Environment.GetEnvironmentVariable("DB_HOST")??"host.docker.internal";
        string port = Environment.GetEnvironmentVariable("DB_PORT")??"5435";
        string db_name = Environment.GetEnvironmentVariable("DB_NAME")??"test_db";
        string user = Environment.GetEnvironmentVariable("DB_USER")??"root";
        string pass = Environment.GetEnvironmentVariable("DB_PASS")??"root";
        optionsBuilder.UseNpgsql("Host="+host+";Port="+port+";Username="+user+";Password="+pass+";Database="+db_name);
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Timestamp).HasDefaultValueSql("now()").HasColumnName("timestamp");
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Numbers).HasColumnName("numbers");
    // }
}