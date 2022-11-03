namespace TaskManager.Controllers;

using Microsoft.AspNetCore.Mvc;
using TaskManager.Service.Data.DbContext;

[ApiController]
[Route("homePage")]
public class HomeController : Controller
{
    private readonly TaskManagerDBContext _context;

    public HomeController(TaskManagerDBContext context)
    {
        _context = context;
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
        return Ok("<h2>Hello, World!</h2>");
    }
}