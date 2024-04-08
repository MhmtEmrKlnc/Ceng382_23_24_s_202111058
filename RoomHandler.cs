using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomHandler
{
    private string? _filePath;

    public List<Room> GetRooms()
    {
        try
        {

            _filePath = "Data.json";

            string jsonString = File.ReadAllText(_filePath);

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            //options yerine new JsonSerializerOptions() yazabiliriz yukarıdakini yazmaya gerek olmadan
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);
            List<Room> rooms = new List<Room>();
            if (roomData?.Rooms != null)
            {
                foreach (var room in roomData.Rooms)
                {

                    if (room.roomId != "" && room.roomName != "" && room.capacity != null)
                    {
                        rooms.Add(room);
                        //Console.WriteLine($"Room Id: {room.roomId}, Name: {room.roomName}, Capacity: {room.capacity}");
                    }

                }
            }
            return rooms;

        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found! - {e.Message}");
            return new List<Room>(); // Hata durumunda boş liste döndür
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"Null exception! - {e.Message}");
            return new List<Room>(); // Hata durumunda boş liste döndür
        }
        catch (Exception e)
        {
            // Diğer hatalar
            Console.WriteLine($"Exception: {e.Message}");
            return new List<Room>(); // Hata durumunda boş liste döndür
        }
    }
    public void SaveRooms(List<Room> Rooms)
    {

        try
        {
            _filePath = "Data.json";
            var existingRooms = GetRooms();
            existingRooms.AddRange(Rooms);

            var roomData = new RoomData { Rooms = existingRooms.ToArray() };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };

            string jsonString = JsonSerializer.Serialize(roomData, options);
            File.WriteAllText(_filePath, jsonString);
            Console.WriteLine("Rooms successfully saved to JSON file.");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found! - {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving Rooms to JSON: {e.Message}");
        }

    }


}