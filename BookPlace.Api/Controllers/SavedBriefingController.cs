using BookPlace.Api.DTOs;
using BookPlace.Api.Extensions;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class SavedBriefingController(ISavedBriefingRepository savedBriefingRepository,IBriefingRepository briefingRepository) : BaseController
{
    [HttpGet("user-{userId}")]
    public async Task<ActionResult<IEnumerable<SavedBriefingListDto>>> GetByUserId(string userId)
    {
        var savedBriefings = await savedBriefingRepository.GetByUserAsync(userId);
        var savedBriefingsDto = savedBriefings.Select(s => s.ToListDto()).ToList();
        return Ok(savedBriefingsDto);
    }
    [HttpGet("brief-{briefingId}")]
    public async Task<ActionResult<IEnumerable<SavedBriefingListDto>>> GetByBriefingId(string briefingId)
    {
        var savedBriefing = await savedBriefingRepository.GetByBriefingsAsync(briefingId);
        var savedBriefingDto = savedBriefing.Select(s => s.ToListDto()).ToList();
        return Ok(savedBriefingDto);
    }

    [HttpGet("{userId}/{briefingId}")]
    public async Task<ActionResult<SavedBriefingListDto>> GetByIds(string userId, string briefingId)
    {
        var savedBriefing = await savedBriefingRepository.GetByIdAsync(userId, briefingId);
        if (savedBriefing == null) return NotFound();
        var savedBriefingDto = savedBriefing.ToListDto();
        return Ok(savedBriefingDto);
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult> CreateSavedBriefing(SavedBriefingCreateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        
        var briefing = await briefingRepository.GetByIdAsync(dto.BriefingId);
        if (briefing == null) return NotFound();

        var existingFav = await savedBriefingRepository.GetByIdAsync(userId, dto.BriefingId);
        if (existingFav != null) return BadRequest("You already saved this briefing.");

        var savedBriefing = new SavedBriefing
        {
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            BriefingId = dto.BriefingId
        };
        await savedBriefingRepository.AddAsync(savedBriefing);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{briefingId}")]
    public async Task<ActionResult> DeleteSavedBriefing(string briefingId)
    {
        var userId = User.GetUserId();
        if (userId == null) return Unauthorized();
        var briefing = await savedBriefingRepository.GetByIdAsync(userId,briefingId);
        if (briefing == null) return NotFound();
        await savedBriefingRepository.DeleteAsync(briefing);
        return Ok();
    }
}