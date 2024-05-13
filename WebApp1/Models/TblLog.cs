using System;
using System.Collections.Generic;

namespace WebApp1.Models;

public partial class TblLog
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Action { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual TblRoom? Room { get; set; }
}
