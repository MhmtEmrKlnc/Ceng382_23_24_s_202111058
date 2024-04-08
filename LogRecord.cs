public record class LogRecord
{
    public string ReserverName{get;set;}
    public string RoomName{get;set;}
    public DateTime Timestamp{get;set;}

    public LogRecord(string reserverName, string roomName, DateTime time){
        ReserverName=reserverName;
        RoomName=roomName;
        Timestamp=time;
    }
}