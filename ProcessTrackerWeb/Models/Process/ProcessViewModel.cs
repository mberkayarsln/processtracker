using System;

namespace ProcessTrackerWeb.Models;

public class ProcessViewModel
{
    public string ProcessName { get; set; }
    public string FolderToApply { get; set; }
    public DateTime RunDate { get; set; }
}