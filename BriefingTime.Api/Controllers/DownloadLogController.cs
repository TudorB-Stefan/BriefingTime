using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BriefingTime.Api.Controllers;

public class DownloadLogController(IDownloadLogRepository downloadLogRepository,IBriefingRepository briefingRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DownloadLogDetailDto>>> GetAll()
    {
        var downloadLogs = await downloadLogRepository.GetAllAsync();
        var downloadLogsDto = downloadLogs.Select(d => d.ToDetailDto()).ToList();
        return Ok(downloadLogsDto);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DownloadLogDetailDto>> GetById(string id)
    {
        var downloadLog = await downloadLogRepository.GetByIdAsync(id);
        if (downloadLog == null) return NotFound();
        var downloadLogDto = downloadLog.ToDetailDto();
        return Ok(downloadLogDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> CreateDownloadLog(DownloadLogCreateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var briefing = await briefingRepository.GetByIdAsync(dto.BriefingId);
        if (briefing == null) return NotFound("Briefing not found.");
        var downloadLog = new DownloadLog
        {
            Id = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            BriefingId = dto.BriefingId
        };
        await downloadLogRepository.AddAsync(downloadLog);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDownloadLog(string id)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var downloadLog = await downloadLogRepository.GetByIdAsync(id);
        if (downloadLog == null) return NotFound();
        await downloadLogRepository.DeleteAsync(downloadLog);
        return Ok();
    }
}