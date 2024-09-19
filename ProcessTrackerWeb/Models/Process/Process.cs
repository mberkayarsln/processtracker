using System;

namespace ProcessTrackerWeb.Models;

public class Process
{
    public int Id { get; set; }
    public string ProcessName { get; set; }
    public string FolderToApply { get; set; }
    public string RunHour { get; set; }
}