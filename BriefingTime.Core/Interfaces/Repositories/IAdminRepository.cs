using BriefingTime.Core.Entities;

namespace BriefingTime.Core.Interfaces.Repositories;

public interface IAdminRepository
{
    Task AddUserDepartmentAsync(UserDepartment userDepartment);
    Task<UserDepartment?> FindById(string userId,string departmentId);
    Task DeleteUserDepartmetn(UserDepartment userDepartment);
    
}