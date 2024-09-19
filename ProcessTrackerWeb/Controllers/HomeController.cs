using Microsoft.AspNetCore.Mvc;
using ProcessTrackerWeb.Services;
using Microsoft.AspNetCore.Http;

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
        var session = HttpContext.Session;

        if (session.GetString("folderPath") is null)
        {
            session.SetString("folderPath", "/Users/arslan/Desktop/Folder");
        }

        return View();
    }

    [HttpPost]
    public void SetFolderPath(string path)
    {
        HttpContext.Session.SetString("folderPath", path);
        _service.StartWatching(path);
    }
}