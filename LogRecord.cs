public record class LogRecord
{
    public string ReserverName{get;set;}
    public string RoomName{get;set;}
    public DateTime Timestamp{get;set;}
    public string Status{get;set;}

    public LogRecord(string ReserverName, string RoomName, DateTime Timestamp, string Status){
        this.ReserverName=ReserverName;
        this.RoomName=RoomName;
        this.Timestamp=Timestamp;
        this.Status=Status;
    }
}