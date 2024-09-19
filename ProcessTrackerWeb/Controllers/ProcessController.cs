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
        var processList = _context.Processes.Select(p => new ProcessViewModel
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
        return View();
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
            FolderToApply = viewModel.FolderToApply,
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