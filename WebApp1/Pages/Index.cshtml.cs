using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp1.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        if(User.Identity.IsAuthenticated){
            return RedirectToPage("/ReservationList");
        }
        else{
            return Redirect("~/Login");
        }
        
        
    }
}
