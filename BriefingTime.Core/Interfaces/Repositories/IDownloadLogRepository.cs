using BriefingTime.Core.Entities;

namespace BriefingTime.Core.Interfaces.Repositories;

public interface IDownloadLogRepository
{
    Task<IEnumerable<DownloadLog>> GetAllAsync();
    Task<DownloadLog?> GetByIdAsync(string id);
    Task AddAsync(DownloadLog downloadLog);
    Task DeleteAsync(DownloadLog downloadLog);
}