using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using Microsoft.VisualBasic;

class Program
{
    static void addReservation(RoomData roomData, ReservationHandler handler, string id, string name, int day, int hour)
    {

        bool check = false;
        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {

                if (id == room.roomId)
                {
                    check = true;
                    if (name == "" || day < 1 || day > 7 || hour < 0 || hour > 24)
                    {
                        Console.WriteLine("Gerekli kısımlar(id, name, day, hour) boş bırakılamaz!\nDay 1-7 arası bir deger, hour 00-23 arası bir deger olmalı!\n");
                    }
                    else if (room.roomId == "" || room.roomName == "" || room.capacity == "")
                    {
                        Console.WriteLine("Boyle bir oda bulunmamaktadır\n");
                    }
                    else
                    {
                        Reservation reservation = new Reservation(id, name, day, hour);
                        reservation.room.roomName = room.roomName;
                        reservation.room.capacity = room.capacity;
                        handler.addReservation(reservation);
                    }

                }
            }
        }

        if (check == false)
        {
            if (id == "")
            {
                Console.WriteLine("id kısmı boş bırakılamaz, lutfen gecerli id'li bir oda giriniz\n");

            }
            else
            {
                Console.WriteLine(id + " id'li bir oda bulunmamaktadır\n");

            }

        }

    }

    static void Main(string[] args)
    {
        try
        {
            //toDo insde try catch
            //path to json into string
            //define file path
            string jsonFilePath = "Data.json";

            //Read from json
            //1->json to text
            //burası kodu patlatabilen bir yer, path'i bulamazsa patlar
            string jsonString = File.ReadAllText(jsonFilePath);

            //2->decode text into meaningful classes

            //options to read

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            //read try catch
            //options yerine new JsonSerializerOptions() yazabiliriz yukarıdakini yazmaya gerek olmadan
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

            if (roomData?.Rooms != null)
            {
                foreach (var room in roomData.Rooms)
                {
                    if (room.roomId != "" && room.roomName != "" && room.capacity != "")
                    {
                        Console.WriteLine($"Room Id: {room.roomId}, Name: {room.roomName}, Capacity: {room.capacity}");
                    }

                }
            }
            Console.WriteLine("\n");
            ReservationHandler reservationHandler = new ReservationHandler(7, 24);

            //json ici bos gelebilir, bu yuzden null olabilir, bu yuzden ? koyuyoruz. Kodu patlatabilir bos gelirse
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
                addReservation(roomData, reservationHandler, "", "", 4, 11); // bos deneme
                addReservation(roomData, reservationHandler, "003", "", 5, 23); // yanlıs degerler deneme
                addReservation(roomData, reservationHandler, "003", "Mehmet", -1, 23); // yanlıs degerler deneme
                addReservation(roomData, reservationHandler, "003", "", 5, 27); // yanlıs degerler deneme
            }

            //json'a yazma denemesi
            var logger = new FileLogger();
            var logHandler = new LogHandler(logger);

            var logRecord1 = new LogRecord("Mehmet Emre", "A-101", DateTime.Now);
            logHandler.AddLog(logRecord1);

            var logRecord2 = new LogRecord("Mehmet Emre Kılınç", "A-102", DateTime.Now);
            logHandler.AddLog(logRecord2);
            //------

            reservationHandler.displayWeeklySchedule();
        }

        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found! - {e.Message}");
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"Null exception! - {e.Message}");
        }
        catch (Exception e)
        {
            // Diğer hatalar
            Console.WriteLine($"Exception: {e.Message}");
        }

    }
}
