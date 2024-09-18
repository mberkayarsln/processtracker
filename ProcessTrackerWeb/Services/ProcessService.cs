using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using ProcessTrackerWeb.Data;

namespace ProcessTrackerWeb.Services;

public class ProcessService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public string RunProcess(int id)
    {
        if (id == 0)
        {
            return "Invalid id";
        }

        var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var process = context.Processes.FirstOrDefault(p => p.Id == id);

        if (process == null)
        {
            return "Process does not exist";
        }

        return $"Process {process.ProcessName} ran successfully";
    }
}