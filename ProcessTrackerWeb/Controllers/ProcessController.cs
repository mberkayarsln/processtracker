using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProcessTrackerWeb.Data;
using ProcessTrackerWeb.Models;
using ProcessTrackerWeb.Services;

namespace ProcessTrackerWeb.Controllers;

public class ProcessController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ProcessService _service;

    public ProcessController(ApplicationDbContext context, ProcessService service)
    {
        _service = service;
        _context = context;
    }

    public IActionResult ListProcess()
    {
        var processList = _context.Processes.Where(p=>p.FolderToApply==HttpContext.Session.GetString("folderPath")).Select(p => new ProcessViewModel
        {
            Id = p.Id,
            ProcessName = p.ProcessName,
            FolderToApply = p.FolderToApply,
            RunHour = p.RunHour
        }).ToList();

        return View(processList);
    }

    public IActionResult AddProcess()
    {
        var viewModel = new ProcessViewModel { FolderToApply = HttpContext.Session.GetString("folderPath") };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddProcess(ProcessViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var process = new Process
        {
            ProcessName = viewModel.ProcessName,
            FolderToApply = HttpContext.Session.GetString("folderPath"),
            RunHour = viewModel.RunHour
        };

        _context.Processes.Add(process);
        _context.SaveChanges();

        return RedirectToAction("ListProcess");
    }

    public IActionResult RunProcessManually(int id)
    {
        _service.RunProcessManually(id);
        return RedirectToAction("ListProcess");
    }
}