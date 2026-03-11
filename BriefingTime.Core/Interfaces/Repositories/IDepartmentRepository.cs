using BriefingTime.Core.Entities;

namespace BriefingTime.Core.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(string id);
    Task<Department?> GetByNameAsync(string name);
    Task AddAsync(Department department);
    Task DeleteAsync(Department department);
}