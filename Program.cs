using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

public class RoomData
{

    [JsonPropertyName("Room")]
    public Room[]? Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string? roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string? roomName { get; set; }

    [JsonPropertyName("capacity")]
    public string? capacity { get; set; }


}

class Program
{
    static void addReservation(RoomData roomData, ReservationHandler handler, string id, string name, int day, int hour)
    {
        Reservation reservation = new Reservation(id, name, day, hour);
        bool check = false;
        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                if (reservation.room != null)
                {
                    if (reservation.room.roomId == room.roomId)
                    {
                        reservation.room.roomName = room.roomName;
                        reservation.room.capacity = room.capacity;
                        check = true;
                        handler.addReservation(reservation);
                    }

                }
            }
        }

        if (check == false)
        {
            Console.WriteLine(id + " id'li bir oda bulunmamaktadır\n");
            handler.deleteReservation(reservation);
        }

    }

    static void Main(string[] args)
    {
        try
        {
            //toDo insde try catch
            //path to json into string
            string jsonFilePath = "Data.json";
            string jsonString = File.ReadAllText(jsonFilePath);

            //options to read

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            //read try catch
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

            if (roomData?.Rooms != null)
            {
                foreach (var room in roomData.Rooms)
                {
                    Console.WriteLine($"Room Id: {room.roomId}, Name: {room.roomName}, Capacity: {room.capacity}");
                }
            }
            Console.WriteLine("\n");
            ReservationHandler reservationHandler = new ReservationHandler(7, 24);

            if (roomData?.Rooms != null)
            {
                addReservation(roomData, reservationHandler, "001", "Mehmet", 5, 6);
                addReservation(roomData, reservationHandler, "003", "Mehmet Emre", 6, 19);
                addReservation(roomData, reservationHandler, "005", "Mehmet Emre", 1, 1);
                addReservation(roomData, reservationHandler, "016", "Can Ates", 3, 1);
                addReservation(roomData, reservationHandler, "016", "Can Ates", 3, 2);
                addReservation(roomData, reservationHandler, "016", "Can Ates", 3, 3);
                addReservation(roomData, reservationHandler, "017", "Can Ates", 5, 3); //olmayan oda denemesi
                addReservation(roomData, reservationHandler, "007", "Mehmet Emre Kılınç", 7, 11);
                addReservation(roomData, reservationHandler, "009", "Mehmet Emre Kılınç", 5, 17);
                addReservation(roomData, reservationHandler, "001", "Mehmet", 5, 7);
                addReservation(roomData, reservationHandler, "001", "Mehmet", 5, 7); // aynısından ekleme denemesi
                addReservation(roomData, reservationHandler, "005", "Mehmet", 4, 11);
            }

            reservationHandler.displayWeeklySchedule();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
