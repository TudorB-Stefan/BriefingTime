using BriefingTime.Core.Entities;

namespace BriefingTime.Core.Interfaces.Repositories;

public interface IBriefingRepository
{
    Task<IEnumerable<Briefing>> GetAllAsync();
    Task<IEnumerable<Briefing>> GetAllForUserAsync(string userId);
    Task<Briefing?> GetByIdAsync(string id);
    Task<Briefing?> GetByIdAsyncForUser(string userId,string briefId);
    Task<Briefing?> GetByIdWithDetailsAsync(string userId,string briefId);
    Task<IEnumerable<Briefing>> GetByUserId(string userId);
    Task AddAsync(Briefing briefing);
    Task UpdateAsync(Briefing briefing);
    Task DeleteAsync(Briefing briefing);
    Task<IEnumerable<Briefing>> GetOldBriefings();
}