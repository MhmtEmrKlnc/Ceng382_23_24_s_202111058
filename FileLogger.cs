using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
public class FileLogger : ILogger
{
    List<LogRecord> _data = new List<LogRecord>();

    public void Log(LogRecord log)
    {
        if (string.IsNullOrEmpty(log.ReserverName) || string.IsNullOrEmpty(log.RoomName) || log.Timestamp == default)
        {
            Console.WriteLine("Hata: ReserverName, RoomName veya Timestamp boş veya geçersiz.");
        }
        else
        {
            _data.Add(log);
            string json = JsonSerializer.Serialize(_data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            if (json == "[]")
            {
                Console.WriteLine("Hata: JSON serialization failed.");
            }
            else
            {
                File.WriteAllText("LogData.json", json);
                Console.WriteLine("Log data LogData.json'a basarılı bir sekilde yazıldı");
            }
        }
    }
}