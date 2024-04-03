public record class LogRecord
{
    public String ReserverName{get;set;}
    public String RoomName{get;set;}
    public DateTime Timestamp{get;set;}

    public LogRecord(string reserverName, string roomName, DateTime time){
        ReserverName=reserverName;
        RoomName=roomName;
        Timestamp=time;

    }

}