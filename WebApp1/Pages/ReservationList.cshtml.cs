﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApp1.Models;

namespace WebApp1.Pages;

public class ReservationListModel : PageModel
{

    public LabWebAppDbContext context = new();
    public static List<TblReservation> ReservationList { get; set; } = new List<TblReservation>();
    public static List<TblRoom> RoomList { get; set; } = default!;
    public static List<IdentityUser> UserList { get; set; } = default!;
    private readonly ILogger<ReservationListModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    [BindProperty(SupportsGet = true)]
    public DateTime DateFilter { get; set; }
    public static DateTime startDate;


    public ReservationListModel(ILogger<ReservationListModel> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public void OnGet()
    {

        var reservations = context.TblReservations.ToList<TblReservation>();
        RoomList = context.TblRooms.ToList<TblRoom>();
        UserList = _userManager.Users.ToList();
        //startDate=DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(1);

        if (DateFilter == DateTime.MinValue)
        {
            startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddDays(1);
        }
        else
        {
            startDate = DateFilter;
        }

        int temp = 0;
        if (ReservationList != null)
        {
            ReservationList.Clear();
        }
        foreach (var reservation in reservations)
        {
            if (DateTime.Compare(reservation.ReservationStartDate, startDate) >= 0 && DateTime.Compare(reservation.ReservationEndDate, startDate.AddDays(7)) <= 0)
            {
                foreach (var reser in ReservationList)
                {
                    temp = 0;
                    if (reservation.Id == reser.Id)
                    {
                        temp = 1;
                        break;
                    }
                }
                if (temp == 0)
                {
                    ReservationList.Add(reservation);
                }

            }
        }

    }

    

}

