using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class DepartmentController(IDepartmentRepository departmentRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentListDto>>> GetAll()
    {
        var departments = await departmentRepository.GetAllAsync();
        var departmentDtos = departments.Select(c => c.ToListDto());
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

    [Authorize]
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
    [Authorize]
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