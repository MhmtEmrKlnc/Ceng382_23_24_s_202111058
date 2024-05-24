﻿using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
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

    public TblLog NewLog { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public DateTime DateFilter { get; set; }
    public static DateTime startDate;

    public static string userId { get; set; }


    public ReservationListModel(ILogger<ReservationListModel> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public void OnGet()
    {
        userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
        var reservations = (from item in context.TblReservations
                            where item.IsDeleted==false
                            select item).ToList();
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

        
        if (ReservationList != null)
        {
            ReservationList.Clear();
        }
        foreach (var reservation in reservations)
        {
            if (DateTime.Compare(reservation.ReservationStartDate, startDate) >= 0 && DateTime.Compare(reservation.ReservationEndDate, startDate.AddDays(7)) <= 0)
            {

                ReservationList.Add(reservation);


            }
        }

    }

    public IActionResult OnPostDelete(int id){
        NewLog = new TblLog();
        if(context.TblReservations!=null){
            var reservation = context.TblReservations.Find(id);
            if(reservation!=null){
                TempData["AlertMessage"]="Reservation Deleted Succesfully!";
                reservation.IsDeleted=true;
                NewLog.UserId=reservation.UserId;
                NewLog.RoomId=reservation.RoomId;
                NewLog.Timestamp=DateTime.Now;
                NewLog.Action="Reservation Deleted";
                context.Add(NewLog);
                context.SaveChanges();
            }
        }
        return RedirectToAction("Get");
    }



}

