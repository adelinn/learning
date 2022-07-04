using dotNetLearn.Models;
using dotNetLearn.Services;

namespace dotNetLearn.Services;

public class RandService
{
    static DBService db = new DBService();

    public List<RandRecord> GetAll() {
        List<RandRecord> randNums = new List<RandRecord>();
        var reader = db.queryGet("select numbers, timestamp from random");
        while (reader.Read()){
            randNums.Add(new RandRecord{Numbers = reader.GetFieldValue<int[]>(0), Timestamp = reader.GetDateTime(1)});
        }
        DBConnectService.close();
        return randNums;
    }
    //public List<RandRecord> Get(DateTime timestamp) => randNums.FindAll(p => p.Timestamp <= timestamp);
    public void Add() {
        RandRecord toBeAdded = new RandRecord();
        for (int i = 0; i < 5; i++){
            toBeAdded.Numbers[i] = new Random().Next();
        }
        List<int[]> toSend = new List<int[]>();
        toSend.Add(toBeAdded.Numbers);
        db.queryPut<int[]>("INSERT INTO random (numbers) VALUES ($1)",toSend);
        //randNums.Add(toBeAdded);
    }
}