using SQLite;

public class InfoItem
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string Name { get; set; }
    public bool Done { get; set; }
}