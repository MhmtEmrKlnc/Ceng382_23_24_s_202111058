using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


public class ReservationRepository : IReservationRepository
{
    private List<Reservation> _reservations = new List<Reservation>();
    public ReservationRepository()
    {
        try
        {
            string jsonFilePath = "ReservationData.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            var reservationList = JsonSerializer.Deserialize<List<Reservation>>(jsonContent);
            _reservations = new List<Reservation>();
            if (reservationList != null)
            {
                foreach (var reservation in reservationList)
                {
                    _reservations.Add(reservation);
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

    public void AddReservation(Reservation reservation)
    {
        try
        {
            if (string.IsNullOrEmpty(reservation.reserverName) || string.IsNullOrEmpty(reservation.room.roomId))
            {
                Console.WriteLine("Hata: ReserverName, RoomName veya Timestamp boş veya geçersiz.");
            }
            else
            {


                _reservations.Add(reservation);

                string json = JsonSerializer.Serialize(_reservations, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                if (json == "[]")
                {
                    Console.WriteLine("Hata: JSON serialization failed.");
                }
                else
                {
                    File.WriteAllText("ReservationData.json", json);
                    Console.WriteLine(reservation.reserverName + " adına " + reservation.room.roomName + " adlı oda icin Reservation basarılı bir sekilde ReservationData.json'a yazıldı");
                }

            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving Rooms to JSON: {e.Message}");
        }

    }


    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);

        try
        {
            _reservations = GetAllReservations();
            _reservations.RemoveAll(r => r.room.roomId == reservation.room.roomId && r.reserverName == reservation.reserverName && r.dateTime == reservation.dateTime && r.time == reservation.time);

            string json = JsonSerializer.Serialize(_reservations, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("ReservationData.json", json);
            Console.WriteLine($"Rezervasyon başarıyla silindi.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Rezervasyon silinirken hata oluştu: {e.Message}");
        }


    }

    public List<Reservation> GetAllReservations()
    {
        return _reservations;
    }
}