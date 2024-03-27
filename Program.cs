using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData{

    [JsonPropertyName("Room")]
    public Room[] ?Rooms {get; set;}
}

public class Room{
    [JsonPropertyName("roomId")]
    public string ?roomId{get;set;}

    [JsonPropertyName("roomName")]
    public string ?roomName{get;set;}

    [JsonPropertyName("capacity")]
    public string ?capacity{get;set;}


}

class Program
{
    static void Main(string[] args)
    {
        //toDo insde try catch
        //path to json into string
        string jsonFilePath = "Data.json";
        string jsonString = File.ReadAllText(jsonFilePath);

        //options to read

        var options = new JsonSerializerOptions(){
            NumberHandling = JsonNumberHandling.AllowReadingFromString |
            JsonNumberHandling.WriteAsString
        };

        //read try catch
        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString,options);

        if(roomData?.Rooms !=null){
            foreach(var room in roomData.Rooms){
                Console.WriteLine($"Room Id: {room.roomId}, Name: {room.roomName}, Capacity: {room.capacity}");
            }
        }
    }
}
