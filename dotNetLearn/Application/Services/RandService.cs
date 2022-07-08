using dotNetLearn.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNetLearn.Services;

public class RandService
{
    private readonly RandDBContext db;
    public RandService(RandDBContext dbCtx){
        db = dbCtx;
        //else db = new RandDBContext(new DbContextOptions<RandDBContext>());
    }

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