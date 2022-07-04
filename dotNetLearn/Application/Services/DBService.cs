using Npgsql;
using System.Data;
using Microsoft.EntityFrameworkCore;
using dotNetLearn.Models;

namespace dotNetLearn.Services;


public class RandContext : DbContext
{
    public DbSet<RandRecord> RandRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=host.docker.internal;Port=5435;Username=root;Password=root;Database=test_db");
}

class DBConnection {
    private readonly string csDocker = "Host=host.docker.internal;Port=5435;Username=root;Password=root;Database=test_db";
    private readonly string csHost = "Host=localhost;Port=5435;Username=root;Password=root;Database=test_db";
    public NpgsqlConnection get(bool tryLocalhost=false){
        NpgsqlConnection connection;
        if(tryLocalhost) connection = new NpgsqlConnection(csHost);
        else connection = new NpgsqlConnection(csDocker);
        return connection;
    }
}
public static class DBConnectService {
    static NpgsqlConnection connection = new DBConnection().get();

    public static void open(){
        try{
            connection.Open();
        }catch{
            connection = new DBConnection().get(true);
            connection.Open();
        }
    }
    public static void close(){
        connection.Close();
    }
    public static NpgsqlConnection conn() => connection;
}

public class DBService {

    public NpgsqlDataReader queryGet<T>(string q, List<(string, T)> p){
        if(DBConnectService.conn().State != ConnectionState.Open) DBConnectService.open();
        var cmd = new NpgsqlCommand(q, DBConnectService.conn());
        if(p!=null) {
            foreach ((string name,T value) parameter in p) {
                cmd.Parameters.AddWithValue(parameter.name, parameter.value);
            }
        }
        var reader = cmd.ExecuteReader();

        // DBConnectService.close();
        return reader;

        // while (await reader.ReadAsync())
        // {
        //     result.Add(reader.GetString(0));
        // }
    }
    public NpgsqlDataReader queryGet<T>(string q, List<T> p){
        DBConnectService.open();
        var cmd = new NpgsqlCommand(q, DBConnectService.conn());
        if(p!=null) {
            foreach (T parameter in p) {
                cmd.Parameters.AddWithValue(parameter);
            }
        }
        var reader = cmd.ExecuteReader();

        // DBConnectService.close();
        return reader;
    }
    public NpgsqlDataReader queryGet(string q){
        DBConnectService.open();
        var cmd = new NpgsqlCommand(q, DBConnectService.conn());
        var reader = cmd.ExecuteReader();

        // DBConnectService.close();
        return reader;
    }

    public void queryPut<T>(string q, List<(string, T)> p){
        DBConnectService.open();
        var cmd = new NpgsqlCommand(q, DBConnectService.conn());
        if(p!=null) {
            foreach ((string name,T value) parameter in p) {
                cmd.Parameters.AddWithValue(parameter.name, parameter.value);
            }
        }
        cmd.ExecuteNonQuery();

        DBConnectService.close();
    }
    public void queryPut<T>(string q, List<T> p){
        DBConnectService.open();
        var cmd = new NpgsqlCommand(q, DBConnectService.conn());
        if(p!=null) {
            foreach (T parameter in p) {
                cmd.Parameters.AddWithValue(parameter);
            }
        }
        cmd.ExecuteNonQuery();

        DBConnectService.close();
    }
    public void queryPut(string q){
        DBConnectService.open();
        var cmd = new NpgsqlCommand(q, DBConnectService.conn());
        cmd.ExecuteNonQuery();

        DBConnectService.close();
    }
}