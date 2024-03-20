using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class AdditionModel : PageModel
{
    private readonly ILogger<AdditionModel> _logger;

    public AdditionModel(ILogger<AdditionModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

