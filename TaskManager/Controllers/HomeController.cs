﻿namespace TaskManager.Controllers;

using Microsoft.AspNetCore.Mvc;
using TaskManager.Service.Data.DbContext;

[ApiController]
[Route("homePage")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return new HtmlResult("<h2>Hello, World!</h2>");
    }
}