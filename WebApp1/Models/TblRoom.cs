using System;
using System.Collections.Generic;

namespace WebApp1.Models;

public partial class TblRoom
{
    public int Id { get; set; }

    public string? RoomName { get; set; }

    public int? PricePerNight { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<TblLog> TblLogs { get; set; } = new List<TblLog>();

    public virtual ICollection<TblReservation> TblReservations { get; set; } = new List<TblReservation>();
}
