using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProcessTrackerWeb.Data;
using ProcessTrackerWeb.Models;

namespace ProcessTrackerWeb.Services;

public class ProcessService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void RunProcessManually(int id)
    {
        if (id == 0)
        {
            Console.WriteLine("Invalid Id");
            return;
        }

        var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var process = context.Processes.FirstOrDefault(p => p.Id == id);

        if (process == null)
        {
            Console.WriteLine("Process does not exist");
            return;
        }

        Console.WriteLine($"Process {process.ProcessName} ran manually");
    }

    public void RunProcessAutomatically(List<Process> processList)
    {
        foreach (var process in processList)
        {
            Console.WriteLine(process.ProcessName + " ran automatically");
        }
    }
}