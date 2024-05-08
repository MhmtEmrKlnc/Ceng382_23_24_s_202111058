using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjeTest.Pages;

public class RoomListModel : PageModel
{

    public AppDbContext RoomListContext= new();
    public static List<Room> RoomList { get;set; } = default!;
    private readonly ILogger<RoomListModel> _logger;

    public RoomListModel(ILogger<RoomListModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
          RoomList = RoomListContext.Rooms.ToList<Room>();
    }
}

