using Microsoft.AspNetCore.Mvc;

namespace ProcessTrackerWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}