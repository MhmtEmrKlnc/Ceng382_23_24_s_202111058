using System.Text.Json;

public class LogService : ILogService
{
    private static List<LogRecord> logs = new List<LogRecord>();
    public void InitializeReservations(string jsonFilePath)
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFilePath);
            logs = JsonSerializer.Deserialize<List<LogRecord>>(
                jsonString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            ) ?? new List<LogRecord>();
        }

        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found! - {e.Message}");
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"Null exception! - {e.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void PrintLogs(List<LogRecord> logs)
    {
        foreach (var log in logs)
        {
            Console.WriteLine($"Day : {log.Timestamp.DayOfWeek}, Hour : {log.Timestamp.Hour}, Reserver : {log.ReserverName}, Room : {log.RoomName} , Status : {log.Status}");
        }
    }
    public void DisplayLogByName(string name)
    {
        var filteredReservations = filterByName(name);
        Console.WriteLine($"\n Logs for {name}");
        PrintLogs(filteredReservations);
    }
    private List<LogRecord> filterByName(string name)
    {
        var filteredLogs = logs.Where(r => r.ReserverName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredLogs;
    }
    public void DisplayLogs(DateTime start, DateTime end)
    {
        if (start.Day < 1 || start.Day > 7 || end.Day < 0 || end.Day > 7)
        {
            Console.WriteLine("O aralıktaki Loglara bakmak istediğiniz günler haftanın günleri yani 1-7 arası olmalıdır");
        }
        else
        {
            var filteredReservations = filterByDate(start, end);
            Console.WriteLine($"\n Logs for {start.DayOfWeek} - {end.DayOfWeek}");
            PrintLogs(filteredReservations);
        }

    }
    private List<LogRecord> filterByDate(DateTime start, DateTime end)
    {
        var filteredLogs = logs.Where(r => r.Timestamp >= start && r.Timestamp <= end).ToList();
        return filteredLogs;
    }
}