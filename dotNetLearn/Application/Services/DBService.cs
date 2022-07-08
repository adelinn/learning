using Microsoft.EntityFrameworkCore;
using dotNetLearn.Models;

namespace dotNetLearn.Services;

public class RandDBContext : DbContext
{
    public DbSet<RandRecord> RandRecords { get; set; }

    RandDBContext(DbContextOptions<RandDBContext> opt): base(opt){
        //
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Timestamp).HasDefaultValueSql("now()").HasColumnName("timestamp");
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Numbers).HasColumnName("numbers");
    // }
}