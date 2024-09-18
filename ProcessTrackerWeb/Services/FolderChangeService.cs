using ProcessTrackerWeb.Data;
using ProcessTrackerWeb.Models.FolderChange;

namespace ProcessTrackerWeb.Services;

public class FolderChangeService
{
    private readonly FileSystemWatcher _watcher;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FolderChangeService(string pathToWatch, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _watcher = new FileSystemWatcher(pathToWatch);
        //_watcher.Changed += OnChanged;
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
            FolderPath = fullPath.Substring(0, fullPath.LastIndexOf("/",StringComparison.Ordinal))
        };

        context.FolderChanges.Add(folderChange);
        context.SaveChanges();
    }
}