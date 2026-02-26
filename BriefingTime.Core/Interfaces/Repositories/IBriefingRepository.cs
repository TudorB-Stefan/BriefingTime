using BriefingTime.Core.Entities;

namespace BriefingTime.Core.Interfaces.Repositories;

public interface IBriefingRepository
{
    Task<IEnumerable<Briefing>> GetAllAsync();
    Task<Briefing?> GetByIdAsync(string id);
    Task<Briefing?> GetByIdWithDetailsAsync(string id);
    Task AddAsync(Briefing briefing);
    Task UpdateAsync(Briefing briefing);
    Task DeleteAsync(Briefing briefing);
}