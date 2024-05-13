using System;
using System.Collections.Generic;

namespace WebApp1.Models;

public partial class TblReservation
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int RoomId { get; set; }

    public DateTime ReservationStartDate { get; set; }

    public DateTime ReservationEndDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual TblRoom Room { get; set; } = null!;
}
