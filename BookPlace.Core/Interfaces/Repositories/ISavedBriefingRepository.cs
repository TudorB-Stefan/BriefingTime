using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface ISavedBriefingRepository
{
    Task<IEnumerable<SavedBriefing>> GetByUserAsync(string userId);
    Task<IEnumerable<SavedBriefing>> GetByBriefingAsync(string briefingId);
    Task<SavedBriefing?> GetByIdAsync(string userId,string briefingId);
    Task AddAsync(SavedBriefing savedBriefing);
    Task DeleteAsync(SavedBriefing savedBriefing);
}