using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages;


public class EditReservationModel : PageModel
{
    [BindProperty]
    public TblReservation reservation { get; set; } = new TblReservation();
    public static List<TblRoom> RoomList { get; set; } = new List<TblRoom>();
    public TblLog NewLog { get; set; } = default!;
    public LabWebAppDbContext context = new();
    private readonly ILogger<EditReservationModel> _logger;

    public EditReservationModel(ILogger<EditReservationModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(int id)
    {
        reservation = context.TblReservations.FirstOrDefault(r => r.Id == id);
        RoomList = context.TblRooms.ToList<TblRoom>();
    }
    public IActionResult OnPost()
    {
        var existingReservation = new TblReservation();
        existingReservation = context.TblReservations.Find(reservation.Id);
        if (existingReservation != null)
        {
            existingReservation.ReservationStartDate = reservation.ReservationStartDate;
            existingReservation.ReservationEndDate = reservation.ReservationEndDate;
            existingReservation.RoomId = reservation.RoomId;
        }
        TempData["AlertMessage"] = "Reservation Updated Succesfully!";
        context.SaveChanges();
        return RedirectToPage("ReservationList");
    }

}

