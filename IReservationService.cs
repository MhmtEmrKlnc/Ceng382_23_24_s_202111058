public interface IReservationService{
    public void AddReservation(Reservation reservation, string ReserverName);
    public void DeleteReservation(Reservation reservation);
    public void DisplayWeeklySchedule();
    
}