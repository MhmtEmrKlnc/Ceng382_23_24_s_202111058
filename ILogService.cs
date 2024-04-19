public interface ILogService{
    public void InitializeReservations(string jsonFilePath);
    public void DisplayLogByName(string name);
    public void DisplayLogs(DateTime start, DateTime end);
    public void PrintLogs(List<LogRecord> logs);
    
}