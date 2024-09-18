using System;

namespace ProcessTrackerWeb.Models.FolderChange;

public class FolderChangeViewModel
{
    public string Path { get; set; }
    public string ChangeType { get; set; }
    public DateTime ChangeDate { get; set; }
}