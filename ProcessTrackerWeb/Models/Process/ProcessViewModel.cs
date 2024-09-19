using System;
using System.ComponentModel.DataAnnotations;

namespace ProcessTrackerWeb.Models;

public class ProcessViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Process name is required")]
    public string ProcessName { get; set; }

    public string? FolderToApply { get; set; }

    [Required(ErrorMessage = "Run hour is required")]
    public string RunHour { get; set; } = DateTime.Now.ToString("HH:mm");
}