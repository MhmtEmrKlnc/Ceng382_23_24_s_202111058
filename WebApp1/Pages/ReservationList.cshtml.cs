﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;

namespace WebApp1.Pages;

public class ReservationListModel : PageModel
{

    public LabWebAppDbContext context= new();
    public static List<TblReservation> ReservationList { get;set; } = default!;
    private readonly ILogger<ReservationListModel> _logger;

    public ReservationListModel(ILogger<ReservationListModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
          ReservationList = context.TblReservations.ToList<TblReservation>();
    }
}

