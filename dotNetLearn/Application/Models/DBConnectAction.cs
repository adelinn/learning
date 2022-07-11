using Microsoft.EntityFrameworkCore;
using dotNetLearn.Services;

namespace dotNetLearn.Models;

public static class DBConnect{
    public static void prepare(DbContextOptionsBuilder o){
        string host = Environment.GetEnvironmentVariable("DB_HOST")??"host.docker.internal";
        if((Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME")??"")!="") host = (Environment.GetEnvironmentVariable("DB_SOCKET_PATH")??"/cloudsql")+"/"+Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME");
        string port = Environment.GetEnvironmentVariable("DB_PORT")??"5435";
        string db_name = Environment.GetEnvironmentVariable("DB_NAME")??"test_db";
        string user = Environment.GetEnvironmentVariable("DB_USER")??"root";
        string pass = Environment.GetEnvironmentVariable("DB_PASS")??"root";
        o.UseNpgsql("Host="+host+";Port="+port+";Username="+user+";Password="+pass+";Database="+db_name);
        //return o;
    }

}