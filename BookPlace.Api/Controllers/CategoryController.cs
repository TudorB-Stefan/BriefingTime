using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class CategoryController(ICategoryRepository categoryRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryListDto>>> GetAll()
    {
        var categories = await categoryRepository.GetAllAsync();
        var categoriesDtos = categories.Select(c => c.ToListDto());
        return Ok(categoriesDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDetailDto>> GetCategoryById(string id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        var categoryDto = category.ToDetailDto();
        return Ok(categoryDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateCategory(CategoryCreateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var category = new Category
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name
        };
        await categoryRepository.AddAsync(category);
        return Ok();
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(string id)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null) return NotFound();
        await categoryRepository.DeleteAsync(category);
        return Ok();
    }
}