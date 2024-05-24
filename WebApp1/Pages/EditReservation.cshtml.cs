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
    public static List<TblReservation> ReservationList { get; set; } = new List<TblReservation>();
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
        NewLog = new TblLog();

        var existingReservation = new TblReservation();
        existingReservation = context.TblReservations.Find(reservation.Id);

        int temp = 0;
        ReservationList = context.TblReservations.ToList();
        foreach (var reservationL in ReservationList)
        {
            temp = 0;
            if (reservationL.ReservationStartDate.Date == reservation.ReservationStartDate.Date)
            {
                if (reservationL.RoomId == reservation.RoomId)
                {

                    if (!(reservation.ReservationStartDate.Hour >= reservationL.ReservationEndDate.Hour || reservation.ReservationEndDate.Hour <= reservationL.ReservationStartDate.Hour))
                    {
                        if (reservationL.Id != reservation.Id)
                        {
                            temp = 1;
                            break;
                        }


                    }

                }

            }


        }
        if (temp != 1)
        {
            if (existingReservation != null)
            {
                existingReservation.ReservationStartDate = reservation.ReservationStartDate;
                existingReservation.ReservationEndDate = reservation.ReservationEndDate;
                existingReservation.RoomId = reservation.RoomId;
            }
            NewLog.UserId = reservation.UserId;
            NewLog.RoomId = reservation.RoomId;
            NewLog.Timestamp = DateTime.Now;
            NewLog.Action = "Reservation Updated";
            context.Add(NewLog);
            TempData["AlertMessage"] = "Reservation Updated Succesfully!";
            context.SaveChanges();
            return RedirectToPage("ReservationList");
        }
        else
        {
            TempData["AlertMessage"] = "There is another reservation for this room covering this time! Please choose another room or time slot.";
            return RedirectToAction("Get");
        }
    }

}

