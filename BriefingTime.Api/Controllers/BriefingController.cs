using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Api.Controllers;

public class BriefingController(IWebHostEnvironment environment,IDepartmentRepository departmentRepository,IBriefingRepository briefingRepository,IMemberRepository memberRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BriefingListDto>>> GetBriefingList()
    {
        var briefings = await briefingRepository.GetAllAsync();
        var briefingDto = briefings.Select(b => b.ToListDto());
        return Ok(briefingDto); 
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BriefingDetailDto>> GetBriefingDetailById(string id)
    {
        var briefing = await briefingRepository.GetByIdAsync(id);
        if (briefing == null) return NotFound();
        var briefingDto = briefing.ToDetailDto();
        return Ok(briefingDto);
    }

    [Authorize]
    [HttpGet("{id}/download")]
    public async Task<IActionResult> DownloadBriefing(string id)
    {
        var book = await briefingRepository.GetByIdAsync(id);
        if (book == null) return NotFound();
        if (string.IsNullOrEmpty(book.FileUrl)) return NotFound("Invalid briefing file path.");
        string filePath = Path.Combine(environment.WebRootPath, "uploads", book.FileUrl);
        if(!System.IO.File.Exists(filePath)) return NotFound("Corrupted briefing file.");
        string contentType = book.ContentType ?? "application/octet-stream";
        string downloadName = book.OriginalFileName ?? book.FileUrl;
        return PhysicalFile(filePath, contentType, downloadName);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> UploadBriefing([FromForm]BriefingCreateDto dto)
    {
        if (dto.File == null || dto.File.Length == 0) return BadRequest("No file uploaded!");
        var allowedExtensions = new[] {".pdf",".epub"};
        var extension = Path.GetExtension(dto.File.FileName).ToLower();
        if (!allowedExtensions.Contains(extension)) return BadRequest("Invalid file extension!");
        
        string uploadFolder = Path.Combine(environment.WebRootPath,"uploads");
        if(!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
        
        string fileId = Guid.NewGuid().ToString() + Path.GetExtension(dto.File.FileName);
        string filePath = Path.Combine(uploadFolder,fileId);
        
        using(var fileStream = new FileStream(filePath, FileMode.Create))
            await dto.File.CopyToAsync(fileStream);
        
        var userId = User.GetUserId();
        var author = await memberRepository.GetByIdAsync(userId);
        
        var department = await departmentRepository.GetByIdAsync(dto.DepartmentId);
        if(department == null) return NotFound("Department not found.");
        
        var briefing = new Briefing
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Author = author.UserName,
            Description = dto.Description,
            FileUrl = fileId,
            OriginalFileName = dto.File.FileName,
            FileSizeBytes = dto.File.Length,
            ContentType = dto.File.ContentType,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            ModifiedAt = DateTime.UtcNow,
            DepartmentId = dto.DepartmentId,
            UserId = userId
        };
        await briefingRepository.AddAsync(briefing);
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBriefing(string id,BriefingUpdateDto dto)
    {
        var briefing = await briefingRepository.GetByIdAsync(id);
        if (briefing == null) return NotFound();
        if (briefing.UserId != User.GetUserId()) return Forbid("You can only edit your own briefings.");

        briefing.Title = dto.Title ?? briefing.Title;
        briefing.Description = dto.Description ?? briefing.Description;
        briefing.DepartmentId = dto.DepartmentId ?? briefing.DepartmentId;
        briefing.ModifiedAt = DateTime.UtcNow;
        
        await briefingRepository.UpdateAsync(briefing);
        return Ok();
    }
    
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteBriefing(string id)
    {
        var briefing = await briefingRepository.GetByIdAsync(id);
        if (briefing == null) return NotFound();
        if (briefing.UserId != User.GetUserId()) return Forbid("You can only delete your own briefings.");
        if (!string.IsNullOrEmpty(briefing.FileUrl))
        {
            var filePath = Path.Combine(environment.WebRootPath, "uploads", briefing.FileUrl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        await briefingRepository.DeleteAsync(briefing);
        return Ok();
    }
}