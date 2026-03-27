using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BriefingTime.Api.Controllers;

public class DepartmentController(IDepartmentRepository departmentRepository) : BaseController
{
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentListDto>>> GetAll()
    {
        var departments = await departmentRepository.GetAllAsync();
        var departmentDtos = departments.Select(d => d.ToListDto()).ToList();
        return Ok(departmentDtos);
    }
    [Authorize]
    [HttpGet("my-departments")]
    public async Task<ActionResult<IEnumerable<DepartmentListDto>>> GetAllForUser()
    {
        var userId = User.GetUserId();
        var departments = await departmentRepository.GetAllAsyncByUser(userId);
        var departmentDtos = departments.Select(d => d.ToListDto());
        return Ok(departmentDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDetailDto>> GetById(string id)
    {
        var department = await departmentRepository.GetByIdAsync(id);
        if (department == null) return NotFound();
        var departmentDto = department.ToDetailDto();
        return Ok(departmentDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Create(DepartmentCreateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var department = new Department
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name
        };
        await departmentRepository.AddAsync(department);
        return Ok();
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var department = await departmentRepository.GetByIdAsync(id);
        if (department == null) return NotFound();
        await departmentRepository.DeleteAsync(department);
        return Ok();
    }
}