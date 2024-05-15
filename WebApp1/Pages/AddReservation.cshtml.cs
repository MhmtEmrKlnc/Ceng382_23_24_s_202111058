﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages;


public class AddReservationModel : PageModel
{
   [BindProperty]
    public TblReservation NewReservation { get; set; } = default!;
    public static List<TblRoom> RoomList { get;set; } = default!;
    public LabWebAppDbContext context= new();
    private readonly ILogger<AddReservationModel> _logger;

    public AddReservationModel(ILogger<AddReservationModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        RoomList = context.TblRooms.ToList<TblRoom>();
    }
    public IActionResult OnPost()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
                NewReservation.UserId = userId;

            /*if (!ModelState.IsValid || NewReservation == null)
            {
                return Page();
            }*/
            NewReservation.IsDeleted = false;
            
            context.Add(NewReservation);
            context.SaveChanges();
            return RedirectToAction("Get");
        }

}

