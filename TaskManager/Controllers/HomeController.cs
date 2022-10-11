using Microsoft.AspNetCore.Mvc;
using TaskManager.Service.DbContext;

namespace TaskManager.Controllers;

[ApiController]
[Route("[controller]")]
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