using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Infrastructure.Repositories;

public class DownloadLogRepository(AppDbContext context) : IDownloadLogRepository
{
    public async Task<IEnumerable<DownloadLog>> GetAllAsync()
    {
        return await context.DownloadLogs.ToListAsync();
    }

    public async Task<DownloadLog?> GetByIdAsync(string id)
    {
        return await context.DownloadLogs.FindAsync(id);
    }

    public async Task AddAsync(DownloadLog downloadLog)
    {
        await context.DownloadLogs.AddAsync(downloadLog);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(DownloadLog downloadLog)
    {
        context.DownloadLogs.Remove(downloadLog);
        await context.SaveChangesAsync();
    }
}