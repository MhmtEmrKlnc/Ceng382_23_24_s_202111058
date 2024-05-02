public class Reservation
{
public int Id { get; set; }
public int reserverId { get; set; }
public int roomId { get; set; }
public DateTime reservationStartDate{ get; set; }
public DateTime reservationEndDate{ get; set; }
public int price{ get; set; }

}