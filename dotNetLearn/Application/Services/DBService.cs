using Microsoft.EntityFrameworkCore;
using dotNetLearn.Models;

namespace dotNetLearn.Services;

public class RandDBContext : DbContext
{
    public DbSet<RandRecord> RandRecords { get; set; }

    public RandDBContext(DbContextOptions<RandDBContext> opt): base(opt){
        //
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Timestamp).HasDefaultValueSql("now()").HasColumnName("timestamp");
    //     modelBuilder.Entity<RandRecord>().Property(e => e.Numbers).HasColumnName("numbers");
    // }
}

public static class DBConnect{
    public static void prepare(DbContextOptionsBuilder o){
        string host = Environment.GetEnvironmentVariable("DB_HOST")??"host.docker.internal";
        if((Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME")??"")!="") host = (Environment.GetEnvironmentVariable("DB_SOCKET_PATH")??"/cloudsql")+"/"+Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");
        string port = Environment.GetEnvironmentVariable("DB_PORT")??"5435";
        string db_name = Environment.GetEnvironmentVariable("DB_NAME")??"test_db";
        string user = Environment.GetEnvironmentVariable("DB_USER")??"root";
        string pass = Environment.GetEnvironmentVariable("DB_PASS")??"root";
        string ssl = (Environment.GetEnvironmentVariable("DB_NOSSL")??"")==""?";Ssl Mode=Require":"";
        if((Environment.GetEnvironmentVariable("DB_CONN_STR")??"")!="")
            o.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONN_STR")??"");
        else o.UseNpgsql("Host="+host+";Port="+port+";Username="+user+";Password="+pass+";Database="+db_name+ssl);
        //return o;
    }

}