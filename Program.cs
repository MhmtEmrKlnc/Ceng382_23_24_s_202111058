using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using Microsoft.VisualBasic;

class Program
{
    static void addReservation(List<Room> rooms, ReservationHandler handler, string id, string name, int day, int hour)
    {

        bool check = false;
        if (rooms != null)
        {
            foreach (var room in rooms)
            {

                if (id == room.roomId)
                {
                    check = true;
                    if (name == "" || day < 1 || day > 7 || hour < 0 || hour > 24)
                    {
                        Console.WriteLine("\nGerekli kısımlar(id, name, day, hour) boş bırakılamaz!\nDay 1-7 arası bir deger, hour 00-23 arası bir deger olmalı!\n");
                    }
                    else if (room.roomId == "" || room.roomName == "" || room.capacity == null)
                    {
                        Console.WriteLine("\nBoyle bir oda bulunmamaktadır\n");
                    }
                    else
                    {
                        Reservation reservation = new Reservation(id, name, day, hour);
                        handler.AddReservation(reservation, reservation.reserverName);

                    }

                }
            }
        }

        if (check == false)
        {
            if (id == "")
            {
                Console.WriteLine("\nid kısmı boş bırakılamaz, lutfen gecerli id'li bir oda giriniz\n");

            }
            else
            {
                Console.WriteLine("\n"+id + " id'li bir oda bulunmamaktadır\n");

            }

        }

    }

    static void Main(string[] args)
    {
        try
        {

            //Odaları Data.json'dan alma
            RoomHandler roomHandler = new RoomHandler();
            var rooms = roomHandler.GetRooms();
            if (rooms != null)
            {
                foreach (var room in rooms)
                {
                    Console.WriteLine($"Room Id: {room.roomId}, Name: {room.roomName}, Capacity: {room.capacity}");
                }
            }



            //Oda ekleme denemesi
            /*var roomsList = new List<Room>
            {
            new Room { roomId = "017", roomName = "A-117", capacity = 10 },
            new Room { roomId = "018", roomName = "Deneme", capacity = 25 }
            // Diğer odaları burada ekleyin
            };

            roomHandler.SaveRooms(roomsList);*/


            Console.WriteLine("\n");


            //reservation repository denemesi
            var repository = new ReservationRepository();
            var logger = new FileLogger();
            var logHandler = new LogHandler(logger);
            ReservationHandler reservationHandler = new ReservationHandler(logHandler, roomHandler, repository);

            if (rooms != null)
            {
                addReservation(rooms, reservationHandler, "001", "Mehmet", 5, 6);
                addReservation(rooms, reservationHandler, "003", "Mehmet Emre", 6, 19);
                addReservation(rooms, reservationHandler, "005", "Mehmet Emre", 1, 1);
                addReservation(rooms, reservationHandler, "016", "Can Ates", 3, 1);
                addReservation(rooms, reservationHandler, "016", "Can Ates", 3, 2);
                addReservation(rooms, reservationHandler, "016", "Can Ates", 3, 3);
                addReservation(rooms, reservationHandler, "017", "Can Ates", 5, 3);
                addReservation(rooms, reservationHandler, "019", "Can Ates", 5, 3);
                addReservation(rooms, reservationHandler, "007", "Mehmet Emre Kilinc", 7, 3);
                addReservation(rooms, reservationHandler, "009", "Mehmet Emre Kilinc", 5, 17);
                addReservation(rooms, reservationHandler, "001", "Mehmet", 5, 7);
                addReservation(rooms, reservationHandler, "005", "Mehmet", 4, 11);
                addReservation(rooms, reservationHandler, "", "", 4, 11); // bos deneme
                addReservation(rooms, reservationHandler, "003", "", 5, 23); // yanlıs degerler deneme
                addReservation(rooms, reservationHandler, "003", "Mehmet", -1, 23); // yanlıs degerler deneme
                addReservation(rooms, reservationHandler, "003", "", 5, 27); // yanlıs degerler deneme
                
            }

            //silme denemesi
            Reservation reservation1 = new Reservation("010", "Silme Denemesi", 7, 14);
            reservationHandler.AddReservation(reservation1, reservation1.reserverName);
            reservationHandler.DeleteReservation(reservation1);



            var allReservations = repository.GetAllReservations();
            if (allReservations != null)
            {
                foreach (var res in allReservations)
                {
                    Console.WriteLine($"Oda ID: {res.room.roomId}, Room Name: {res.room.roomName}, Rezervasyon Yapan: {res.reserverName}, Gün: {res.dateTime.DayOfWeek + "(" + res.dateTime.Day + ")"}, Saat: {res.time.Hour}");
                }
            }


            ReservationService reservationService = new ReservationService(reservationHandler);
            reservationService.DisplayWeeklySchedule();

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
