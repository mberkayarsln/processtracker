namespace ProcessTrackerWeb.Models.FolderChange;

public class FolderChange
{
    public int Id { get; set; }
    public string FolderPath { get; set; }
    public string FolderName { get; set; }
    public string FileName { get; set; }
    public string ChangeType { get; set; }
    public DateTime ChangeDate { get; set; }
}