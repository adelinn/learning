namespace dotNetLearn.Models;

public class RandRecord {
    public int[] Numbers { get; set; }
    public DateTime? Timestamp { get; set; }
    public RandRecord(){
        Numbers = new int[5];
    }
    public RandRecord(int[] n, DateTime t){
        Numbers = n;
        Timestamp = t;
    }
}