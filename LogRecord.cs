public record class LogRecord
{
    public string ReserverName{get;set;}
    public string RoomName{get;set;}
    public DateTime Timestamp{get;set;}

    public LogRecord(string ReserverName, string RoomName, DateTime Timestamp){
        this.ReserverName=ReserverName;
        this.RoomName=RoomName;
        this.Timestamp=Timestamp;
    }
}