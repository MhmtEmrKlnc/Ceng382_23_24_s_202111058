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
    public TblLog NewLog { get; set; } = default!;
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
            NewLog=new TblLog();
            NewLog.RoomId=NewReservation.RoomId;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null){
                NewReservation.UserId = userId;
                NewLog.UserId = userId;
            }
            
            NewReservation.IsDeleted = false;

            NewLog.Timestamp=DateTime.Now;
            NewLog.IsDeleted = false;
            NewLog.Action="Reservation Added";

            
            context.Add(NewLog);
            context.Add(NewReservation);
            context.SaveChanges();
            
            return RedirectToAction("Get");
        }

}

