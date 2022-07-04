using dotNetLearn.Models;
using dotNetLearn.Services;

namespace dotNetLearn.Services;

public class RandService
{
    static RandDBContext db = new RandDBContext();

    public List<RandRecord> GetAll() => db.RandRecords.ToList();
    public List<RandRecord> Get(DateTime timestamp) => db.RandRecords.Where(p => p.Timestamp <= timestamp).ToList();
    public void Add() {
        RandRecord toBeAdded = new RandRecord();
        for (int i = 0; i < 5; i++){
            toBeAdded.Numbers[i] = new Random().Next();
        }
        db.RandRecords.Add(toBeAdded);
        db.SaveChanges();
    }
}