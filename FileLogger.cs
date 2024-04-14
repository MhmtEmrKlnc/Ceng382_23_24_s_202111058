using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
public class FileLogger : ILogger
{
    List<LogRecord> _data = new List<LogRecord>();
    public FileLogger()
    {
        try
        {
            string jsonFilePath = "LogData.json";
            string jsonContent = File.ReadAllText(jsonFilePath);

            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString,
                IncludeFields = true
            };

            var logs = JsonSerializer.Deserialize<List<LogRecord>>(jsonContent, options);
            if (logs != null)
            {
                foreach (var log in logs)
                {
                    _data.Add(log);
                }
            }
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
    public void Log(LogRecord log)
    {
        try
        {
            if (log == null)
            {
                Console.WriteLine("Exception: log boş");
                return;
            }
            if (string.IsNullOrEmpty(log.ReserverName) || string.IsNullOrEmpty(log.RoomName))
            {
                Console.WriteLine("Hata: ReserverName veya RoomName boş veya geçersiz.");
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
                    try
                    {
                        File.WriteAllText("LogData.json", json);
                        Console.WriteLine("Log data LogData.json'a basarılı bir sekilde yazıldı\n");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Hata: Dosya yazma işlemi sırasında bir I/O hatası oluştu: {ex.Message}");
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine($"Hata: Dosyaya erişim izni yok: {ex.Message}");
                    }


                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }


    }
}