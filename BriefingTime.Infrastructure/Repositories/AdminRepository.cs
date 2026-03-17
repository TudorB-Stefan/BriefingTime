using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Infrastructure.Repositories;

public class AdminRepository(AppDbContext context) : IAdminRepository
{
    public async Task AddUserDepartmentAsync(UserDepartment userDepartment)
    {
        await context.UserDepartments.AddAsync(userDepartment);
        await context.SaveChangesAsync();
    }

    public async Task<UserDepartment?> FindById(string userId,string departmentId)
    {
        return await context.UserDepartments.FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DepartmentId == departmentId);
    }

    public async Task DeleteUserDepartmetn(UserDepartment userDepartment)
    {
        context.UserDepartments.Remove(userDepartment);
        await context.SaveChangesAsync();
    }
}