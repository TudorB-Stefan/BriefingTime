using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Api.DTOs.MemberDtos;
using BriefingTime.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BriefingTime.Api.Controllers;

public class MemberController(IMemberRepository memberRepository) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberListDto>>> GetAllMembers()
    {
        var users = await memberRepository.GetAllAsync();
        var memebersDto = users.Select(u => u.ToListDto()).ToList();
        return Ok(memebersDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDetailDto>> GetById(string id)
    {
        var user = await memberRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        var memberDto = user.ToDetailDto();
        return Ok(memberDto);
    }

    [HttpGet("username-{username}")]
    public async Task<ActionResult<MemberDetailDto>> GetByUserName(string username)
    {
        var user = await memberRepository.GetByUsername(username);
        if (user == null) return NotFound();
        var memberDto = user.ToDetailDto();
        return Ok(memberDto);
    }
}