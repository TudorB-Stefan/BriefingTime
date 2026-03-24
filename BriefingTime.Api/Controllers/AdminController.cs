using System.Linq.Expressions;
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

    [HttpDelete("department/{id}")]
    public async Task<ActionResult> DeleteUserDepartment(string userId, string departmentId)
    {
        var userDepartment = await adminRepository.FindById(userId, departmentId);
        if (userDepartment == null) return NotFound();
        await adminRepository.DeleteUserDepartmetn(userDepartment);
        return Ok();
    }
}