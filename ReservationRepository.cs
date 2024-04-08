public class ReservationRepository : IReservationRepository
{
    private List<Reservation> _reservations;
    public ReservationRepository()
    {
        _reservations = new List<Reservation>();
    }

    public void AddReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public void DeleteReservation(Reservation reservation)
    {
        throw new NotImplementedException();
    }

    public List<Reservation> GetAllReservations()
    {
        throw new NotImplementedException();
    }
}