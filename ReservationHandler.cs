public record class ReservationHandler
{
    private readonly ReservationService _reservationService;
    private readonly LogHandler _logHandler;
    private readonly RoomHandler _roomHandler;
    public ReservationHandler(LogHandler logHandler, RoomHandler roomHandler, ReservationService reservationService)
    {
        _reservationService = reservationService;
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
            var _reservations = _reservationService.GetAllReservations();
            bool check = false;
            foreach (var _reservation in _reservations)
            {
                check = false;
                if (_reservation.dateTime == reservation.dateTime && _reservation.time == reservation.time)
                {
                    check = true;//aynısından var
                    break;
                }
            }
            if (check == true)
            {
                Console.WriteLine("Bu saat ve günde başka bir rezervasyon var, lütfen başka saat-gün seçin\n");
            }
            else
            {
                //ReservationData.jsonda şu anda var olan rezervasyonlar var. Bu yüzden bir rezervasyon silinirse buradan da silinir.
                _reservationService.AddReservation(reservation);
                DateTime logDateTime = new DateTime(1, 1, reservation.dateTime.Day, reservation.time.Hour, 00, 0);
                //LogData.jsonda geçmişe yönelik bütün rezervasyon kayıtları var. Bir rezervasyon silinirse buradan silinmez çünkü öyle bir rezervasyon yapıldı önceden.
                var logRecord = new LogRecord(ReserverName, reservation.room.roomName, logDateTime, "Rezervasyon Eklendi");
                _logHandler.AddLog(logRecord);
            }

        }


    }
    public void DeleteReservation(Reservation reservation)
    {
        if (reservation.reserverName!=null && reservation.room.roomName!=null)
        {
            _reservationService.DeleteReservation(reservation);
            DateTime logDateTime = new DateTime(1, 1, reservation.dateTime.Day, reservation.time.Hour, 00, 0);
            var logRecord = new LogRecord(reservation.reserverName, reservation.room.roomName, logDateTime, "Rezervasyon Silindi");
            _logHandler.AddLog(logRecord);
        }
    }
    public List<Reservation> GetAllReservations()
    {
        return _reservationService.GetAllReservations();
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