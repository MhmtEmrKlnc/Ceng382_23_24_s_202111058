public interface IReservationService{
    public void InitializeReservations();
    public void PrintReservations(List<Reservation> reservations);
    public void AddReservation(Reservation reservation);
    public void DeleteReservation(Reservation reservation);
    public void DisplayWeeklySchedule();
    public void DisplayReservationByReserver(string name);
    public void DisplayReservationByRoomId(string Id);
    
}