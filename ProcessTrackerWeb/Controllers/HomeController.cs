using Microsoft.AspNetCore.Mvc;
using ProcessTrackerWeb.Services;

namespace ProcessTrackerWeb.Controllers;

public class HomeController : Controller
{
    private readonly FolderChangeService _service;

    public HomeController(FolderChangeService service)
    {
        _service = service;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}