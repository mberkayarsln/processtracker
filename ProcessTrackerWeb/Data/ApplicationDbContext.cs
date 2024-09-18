using Microsoft.EntityFrameworkCore;
using ProcessTrackerWeb.Models;
using ProcessTrackerWeb.Models.FolderChange;

namespace ProcessTrackerWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<FolderChange> FolderChanges { get; set; }
    public DbSet<Process> Processes { get; set; }
}