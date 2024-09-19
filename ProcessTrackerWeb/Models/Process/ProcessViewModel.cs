using System;
using System.ComponentModel.DataAnnotations;

namespace ProcessTrackerWeb.Models;

public class ProcessViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Process name is required")]
    public string ProcessName { get; set; }

    [Required(ErrorMessage = "Folder is required")]
    public string FolderToApply { get; set; } = "/Users/arslan/Desktop/Folder";

    [Required(ErrorMessage = "Run hour is required")]
    public string RunHour { get; set; }
}