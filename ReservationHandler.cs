public record class ReservationHandler
{
    private readonly IReservationRepository _reservationRepository;
    private readonly LogHandler _logHandler;
    private readonly RoomHandler _roomHandler;
    public ReservationHandler(LogHandler logHandler, RoomHandler roomHandler, IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
        _logHandler = logHandler;
        _roomHandler = roomHandler;

    }

    public void AddReservation(Reservation reservation, string ReserverName)
    {
        var rooms = _roomHandler.GetRooms();
        if (rooms != null)
        {
            foreach (var room in rooms)
            {

                if (reservation.room.roomId == room.roomId)
                {

                    reservation.room.roomName = room.roomName;
                    reservation.room.capacity = room.capacity;

                }
            }
        }

        if (string.IsNullOrEmpty(reservation.room.roomName) || string.IsNullOrEmpty(ReserverName))
        {
            Console.WriteLine("Hata: Room Name, Reserver Name bos veya gecersiz");
        }
        else
        {
            _reservationRepository.AddReservation(reservation);
            var logRecord = new LogRecord(ReserverName, reservation.room.roomName, reservation.dateTime);
            _logHandler.AddLog(logRecord);
        }


    }
    public void DeleteReservation(Reservation reservation)
    {
        _reservationRepository.DeleteReservation(reservation);
    }
    public List<Reservation> GetAllReservations()
    {
        return _reservationRepository.GetAllReservations();
    }
    public List<Room> GetRooms()
    {
        return _roomHandler.GetRooms();
    }
    public void SaveRooms(List<Room> rooms)
    {
        _roomHandler.SaveRooms(rooms);
    }


}