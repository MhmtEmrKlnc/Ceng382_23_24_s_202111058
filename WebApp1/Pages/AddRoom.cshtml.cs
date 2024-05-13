﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Pages;

public class AddRoomModel : PageModel
{
   [BindProperty]
    public TblRoom NewRoom { get; set; } = default!;

    public LabWebAppDbContext context= new();
    private readonly ILogger<AddRoomModel> _logger;

    public AddRoomModel(ILogger<AddRoomModel> logger)
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
            NewRoom.IsDeleted = false;
            context.Add(NewRoom);
            context.SaveChanges();
            return RedirectToAction("Get");
        }

}

