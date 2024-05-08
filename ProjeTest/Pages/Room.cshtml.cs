using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjeTest.Pages;

public class RoomModel : PageModel
{
   [BindProperty]
    public Room NewRoom { get; set; } = default!;

    public AppDbContext RoomContext= new();
    private readonly ILogger<RoomModel> _logger;

    public RoomModel(ILogger<RoomModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
    public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewRoom == null)
            {
                return Page();
            }
            RoomContext.Add(NewRoom);
            RoomContext.SaveChanges();
            return RedirectToAction("Get");
        }

}

