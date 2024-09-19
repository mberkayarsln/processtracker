using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProcessTrackerWeb.Data;
using ProcessTrackerWeb.Models.FolderChange;

namespace ProcessTrackerWeb.Controllers;

public class FolderChangeController : Controller
{
    private readonly ApplicationDbContext _context;

    public FolderChangeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult ListChange()
    {
        var changeList = _context.FolderChanges.Where(c => c.FolderPath == HttpContext.Session.GetString("folderPath"))
            .Select(f => new FolderChangeViewModel
            {
                ChangeDate = f.ChangeDate,
                ChangeType = f.ChangeType,
                Path = f.FolderPath + "/" + f.FileName
            }).ToList();

        return View(changeList);
    }
}