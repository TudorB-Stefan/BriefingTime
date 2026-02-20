using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface IDownloadLogRepository
{
    Task<IEnumerable<DownloadLog>> GetAllAsync();
    Task<DownloadLog?> GetByIdAsync(string id);
    Task AddAsync(DownloadLog downloadLog);
    Task DeleteAsync(DownloadLog downloadLog);
}