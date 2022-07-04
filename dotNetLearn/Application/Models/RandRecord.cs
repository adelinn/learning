using System.ComponentModel.DataAnnotations.Schema;

namespace dotNetLearn.Models;

[Table("random")]
public class RandRecord {
    [Column("idNo")]
    public int Id {get; set;}

    [Column("numbers")]
    public int[] Numbers { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("timestamp")]
    public DateTime? Timestamp { get; set; }
    public RandRecord(){
        Numbers = new int[5];
    }
    public RandRecord(int[] n, DateTime t){
        Numbers = n;
        Timestamp = t;
    }
}