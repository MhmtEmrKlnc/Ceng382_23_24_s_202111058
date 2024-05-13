﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp1.Models;

namespace WebApp1.Pages;

public class RoomListModel : PageModel
{

    public LabWebAppDbContext context= new();
    public static List<TblRoom> RoomList { get;set; } = default!;
    private readonly ILogger<RoomListModel> _logger;

    public RoomListModel(ILogger<RoomListModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
          RoomList = context.TblRooms.ToList<TblRoom>();
    }
}

