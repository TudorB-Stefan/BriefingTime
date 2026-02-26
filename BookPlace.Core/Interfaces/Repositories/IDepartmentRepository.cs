using BookPlace.Core.Entities;

namespace BookPlace.Core.Interfaces.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(string id);
    Task AddAsync(Department department);
    Task DeleteAsync(Department department);
}