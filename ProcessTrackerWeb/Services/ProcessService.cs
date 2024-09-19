using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using ProcessTrackerWeb.Data;
using ProcessTrackerWeb.Models;

namespace ProcessTrackerWeb.Services;

public class ProcessService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer _timer;

    public ProcessService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        
        int delayToNextMinute = (60 - DateTime.Now.Second) * 1000;
        _timer = new Timer(RunProcessPeriodically, null, delayToNextMinute, 60000);
    }

    private void RunProcess(string processName, string runType)
    {
        Console.WriteLine(processName + " ran successfully - " + runType);
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

        RunProcess(process.ProcessName, "Manual");
    }

    public void RunProcessAutomatically(List<Process> processList)
    {
        foreach (var process in processList)
        {
            RunProcess(process.ProcessName, "Automatic");
        }
    }

    private void RunProcessPeriodically(object? state)
    {
        Console.WriteLine("WORKED");
        var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var processList = context.Processes.ToList();

        foreach (var process in processList)
        {
            if (process.RunHour == DateTime.Now.ToString("HH:mm"))
            {
                RunProcess(process.ProcessName, "Periodic");
            }
        }
    }
}