using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ProcessTrackerWeb.Data;
using ProcessTrackerWeb.Models.FolderChange;

namespace ProcessTrackerWeb.Services;

public class FolderChangeService
{
    private FileSystemWatcher _watcher;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ProcessService _service;

    public FolderChangeService(IServiceScopeFactory serviceScopeFactory, ProcessService service)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _service = service;
    }

    public void StartWatching(string pathToWatch)
    {
        _watcher = new FileSystemWatcher(pathToWatch);
        _watcher.Created += OnChange;
        _watcher.Deleted += OnChange;
        _watcher.Renamed += OnRename;
        _watcher.EnableRaisingEvents = true;
    }

    private void OnChange(object source, FileSystemEventArgs e)
    {
        SaveToDatabase(e.FullPath, e.ChangeType.ToString());
    }

    private void OnRename(object source, RenamedEventArgs e)
    {
        SaveToDatabase(e.FullPath, "Renamed");
    }

    private void SaveToDatabase(string fullPath, string changeType)
    {
        var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var folderChange = new FolderChange
        {
            ChangeDate = DateTime.Now,
            ChangeType = changeType,
            FileName = fullPath.Split("/")[^1],
            FolderName = fullPath.Split("/")[^2],
            FolderPath = fullPath.Substring(0, fullPath.LastIndexOf("/", StringComparison.Ordinal))
        };

        context.FolderChanges.Add(folderChange);
        context.SaveChanges();

        var processList = context.Processes.Where(p => p.FolderToApply == folderChange.FolderPath).ToList();
        if (!processList.Any())
            return;
        _service.RunProcessAutomatically(processList);
    }
}