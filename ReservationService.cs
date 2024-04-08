public class ReservationService : IReservationService
{
    private ReservationHandler _reservationHandler;

    public ReservationService(ReservationHandler reservationHandler)
    {
        _reservationHandler = reservationHandler;
    }

    public void AddReservation(Reservation reservation, string ReserverName)
    {
        _reservationHandler.AddReservation(reservation,ReserverName);
    }

    public void DeleteReservation(Reservation reservation)
    {
        _reservationHandler.DeleteReservation(reservation);
    }

    public void DisplayWeeklySchedule()
    {
        var reservations = _reservationHandler.GetAllReservations();
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