﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;

namespace WebApp1.Pages;

public class ReservationListModel : PageModel
{

    public LabWebAppDbContext context= new();
    public static List<TblReservation> ReservationList { get;set; } = default!;
    public static List<TblRoom> RoomList { get;set; } = default!;
    public static List<IdentityUser> UserList { get;set; } = default!;
    private readonly ILogger<ReservationListModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public static DateOnly startDate;

    public ReservationListModel(ILogger<ReservationListModel> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public void OnGet()
    {
        ReservationList = context.TblReservations.ToList<TblReservation>();
        RoomList=context.TblRooms.ToList<TblRoom>();
        UserList=_userManager.Users.ToList();
        startDate=DateOnly.FromDateTime(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek));
        
    }
}

