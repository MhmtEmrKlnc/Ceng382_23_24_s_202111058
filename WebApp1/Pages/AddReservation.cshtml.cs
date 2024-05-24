using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages;


public class AddReservationModel : PageModel
{
    [BindProperty]
    public TblReservation NewReservation { get; set; } = default!;
    public static List<TblReservation> ReservationList { get; set; } = default!;
    public TblLog NewLog { get; set; } = default!;
    public static List<TblRoom> RoomList { get; set; } = default!;
    public LabWebAppDbContext context = new();
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
        int temp = 0;
        ReservationList = context.TblReservations.ToList();
        foreach (var reservation in ReservationList)
        {
            temp = 0;
            if (reservation.ReservationStartDate.Date == NewReservation.ReservationStartDate.Date)
            {
                if (reservation.RoomId == NewReservation.RoomId)
                {

                    if (!(NewReservation.ReservationStartDate.Hour >= reservation.ReservationEndDate.Hour || NewReservation.ReservationEndDate.Hour <= reservation.ReservationStartDate.Hour))
                    {
                        temp = 1;
                        break;

                    }

                }

            }


        }
        if (temp != 1)
        {
            if (NewReservation.ReservationStartDate == NewReservation.ReservationEndDate)
            {
                NewLog = new TblLog();
                NewLog.RoomId = NewReservation.RoomId;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {
                    NewReservation.UserId = userId;
                    NewLog.UserId = userId;
                }

                NewReservation.IsDeleted = false;

                NewLog.Timestamp = DateTime.Now;
                NewLog.IsDeleted = false;
                NewLog.Action = "Reservation Added";


                context.Add(NewLog);
                context.Add(NewReservation);
                TempData["AlertMessage"] = "Reservation Added Succesfully!";
                context.SaveChanges();

                return Redirect("/ReservationList");
            }
            else
            {
                TempData["AlertMessage"] = "Reservation start date and end date must be the same day";
                return RedirectToAction("Get");
            }

        }
        else
        {
            TempData["AlertMessage"] = "There is another reservation for this room covering this time! Please choose another room or time slot.";
            return RedirectToAction("Get");
        }



    }

}

