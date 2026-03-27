using System.Linq.Expressions;
using System.Security.Claims;
using BriefingTime.Api.DTOs.AdminDto;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BriefingTime.Api.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(UserManager<User> userManager,IAdminRepository adminRepository,IDepartmentRepository departmentRepository,IMemberRepository memberRepository) : BaseController
{
    [HttpPost("assign-department")]
    public async Task<ActionResult> AssignUserDepartment([FromBody] AssignDepartmentDto assignDepartmentDto)
    {
        var user = await memberRepository.GetByIdAsync(assignDepartmentDto.UserId);
        if(user==null) return BadRequest("Invalid User ID or Department ID");
        var department = await departmentRepository.GetByIdAsync(assignDepartmentDto.DepartmentId);
        if (department==null) return BadRequest("Invalid User ID or Department ID");
        var old = await adminRepository.FindById(user.Id, department.Id);
        if (old != null) return BadRequest("User is already in this department.");
        UserDepartment userDepartment = new UserDepartment
        {
            UserId = assignDepartmentDto.UserId,
            DepartmentId = assignDepartmentDto.DepartmentId
        };
        await adminRepository.AddUserDepartmentAsync(userDepartment);
        return Ok();
    }

    [HttpDelete("department/{userId}/{departmentId}")]
    public async Task<ActionResult> DeleteUserDepartment(string userId, string departmentId)
    {
        var userDepartment = await adminRepository.FindById(userId, departmentId);
        if (userDepartment == null) return NotFound();
        await adminRepository.DeleteUserDepartmetn(userDepartment);
        return Ok();
    }

    [HttpPost("{userId}/make-admin")]
    public async Task<ActionResult> MakeAdmin(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(new { message = "User not found." });
        
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            var result = await userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded) return BadRequest(new { message = "Failed to add Admin role." });
        }
        
        return Ok();
    }

    [HttpDelete("{userId}/remove-admin")]
    public async Task<ActionResult> RemoveAdmin(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(new { message = "User not found." });
        
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("nameid");
        if (userId == currentUserId)
        {
            return BadRequest(new { message = "You cannot remove your own Admin privileges." });
        }

        if (await userManager.IsInRoleAsync(user, "Admin"))
        {
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");
            if (!result.Succeeded) return BadRequest(new { message = "Failed to remove Admin role." });
        }
        
        return Ok();
    }
}