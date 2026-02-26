using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Infrastructure.Repositories;

public class DepartmentRepository(AppDbContext context) : IDepartmentRepository
{
    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await context.Departments
            .Include(d => d.Briefings)
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(string id)
    {
        return await context.Departments
            .Include(d => d.Briefings)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Department department)
    {
        await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Department department)
    {
        context.Departments.Remove(department);
        await context.SaveChangesAsync();
    }
}