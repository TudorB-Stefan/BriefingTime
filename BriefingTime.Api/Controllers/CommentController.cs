using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using BriefingTime.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Api.Controllers;

public class CommentController(ICommentRepository commentRepository,IBriefingRepository briefingRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDetailDto>>> GetAll()
    {
        var comments = await commentRepository.GetAllAsync();
        var commentDtos = comments.Select(c => c.ToDetailDto()).ToList();
        return Ok(commentDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDetailDto>> GetById(string id)
    {
        var comment = await commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();
        var commentDto = comment.ToDetailDto();
        return Ok(commentDto);
    }

    [HttpGet("comment-brief-{briefingId}")]
    public async Task<ActionResult<IEnumerable<CommentDetailDto>>> GetByBriefing(string briefingId)
    {
        var comments = await commentRepository.GetByBriefing(briefingId);
        var commentDto = comments.Select(r => r.ToDetailDto()).ToList();
        return Ok(commentDto);
    }
    
    [HttpGet("comment-user-{userId}")]
    public async Task<ActionResult<IEnumerable<CommentDetailDto>>> GetByUser(string userId)
    {
        var comments = await commentRepository.GetByUser(userId);
        var commentDto = comments.Select(r => r.ToDetailDto()).ToList();
        return Ok(commentDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateComment(CommentCreateDto dto)
    {
        var briefing = await briefingRepository.GetByIdAsync(dto.BriefingId);
        var userId = User.GetUserId();
        if (briefing == null) return NotFound("Briefing not found.");
        var comment = new Comment
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            UserId = userId,
            BriefingId = dto.BriefingId
        };
        await commentRepository.AddAsync(comment);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult> UpdateComment(string id,CommentUpdateDto dto)
    {
        var userId = User.GetUserId();
        if (userId == null) return NotFound("User not found");
        var comment = await commentRepository.GetByIdAsync(id);
        if(comment == null) return NotFound("Comment not found");
        if (comment.UserId != userId) return Forbid("You can only edit your own comments.");
        comment.Description = dto.Description;
        comment.Title = dto.Title;
        await commentRepository.UpdateAsync(comment);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> DeleteComment(string id)
    {
        var comment = await commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();
        if (comment.UserId != User.GetUserId()) return Forbid("You can only edit your own comments.");
        await commentRepository.DeleteAsync(comment);
        return Ok();
    }
}