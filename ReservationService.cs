public class ReservationService : IReservationService
{
    private ReservationRepository _reservationRepository;
    private static List<Reservation> reservations = new List<Reservation>();

    public void InitializeReservations()
    {
        //sizin koydugunuz gibi jsonfilepath alan şekilde yapmadım cünkü zaten reservationRepository jsondan reservasyonları alma islemini yapıyor ve burada da GetAllReservations ile bu rezervayonları alabiliriz
        reservations =_reservationRepository.GetAllReservations();
    }
    public void PrintReservations(List<Reservation> reservations)
    {
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"Day : {reservation.dateTime.DayOfWeek}, Hour : {reservation.time.Hour}, Reserver : {reservation.reserverName}, Room : {reservation.room.roomName} , Capacity : {reservation.room.capacity}");
        }
    }
    public void DisplayReservationByReserver(string name)
    {
        var filteredReservations = filterByName(name);
        Console.WriteLine($"\n Reservations for {name}");
        PrintReservations(filteredReservations);
    }

    private List<Reservation> filterByName (string name)
    {
        var filteredReservations = reservations.Where(r => r.reserverName.Equals(name,StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredReservations;
    }
    public ReservationService(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
    public void DisplayReservationByRoomId(string Id)
    {
        //todo do not print room ıd print name of the Room for given id 
        var filteredReservations = filterByRoomId(Id);
        Console.WriteLine($"\n Reservations for RoomId {Id}");
        PrintReservations(filteredReservations);
    }
    private List<Reservation> filterByRoomId ( string Id)
    {
        var filteredReservations = reservations.Where(r => r.room.roomId.Equals(Id,StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredReservations;
    }

    public void AddReservation(Reservation reservation)
    {
        _reservationRepository.AddReservation(reservation);
    }

    public void DeleteReservation(Reservation reservation)
    {
        _reservationRepository.DeleteReservation(reservation);
    }
    public List<Reservation> GetAllReservations()
    {
        return _reservationRepository.GetAllReservations();
    }

    public void DisplayWeeklySchedule()
    {
        var reservations = _reservationRepository.GetAllReservations();
        Console.WriteLine("\nTablodaki satırlar günleri, sütunlar saatleri göstermektedir. Tablo o gün ve saate denk gelen, rezervasyonu olan odanın id'sini göstermektedir.");
        Console.Write("   |");
        for (int j = 0; j < 24; j++)
        {
            Console.Write(j.ToString("D2") + ":00|");
        }
        Console.Write("\n");
        for (int i = 1; i < 8; i++)
        {
            Console.Write(i + " | ");
            for (int j = 0; j < 24; j++)
            {
                bool check = false;
                foreach (var reservation in reservations)
                {

                
                    if (reservation.dateTime.Day == i && reservation.time.Hour == j)
                    {

                        Console.Write(reservation.room.roomName + "|");
                        check = true;
                    }

                }
                if (check == false)
                {
                    Console.Write(" --- |");
                }


            }
            Console.Write("\n");
        }



    }
}