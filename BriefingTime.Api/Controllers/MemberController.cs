using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Api.DTOs.AdminDto;
using BriefingTime.Api.DTOs.MemberDtos;
using BriefingTime.Core.Entities;
using BriefingTime.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BriefingTime.Api.Controllers;

public class MemberController(IMemberRepository memberRepository,UserManager<User> userManager) : BaseController
{
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDepartmentsDto>>> GetAllMembers()
    {
        var users = await memberRepository.GetAllAsync();
        
        var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
        var adminIds = adminUsers.Select(a => a.Id).ToHashSet();
        
        var memebersDto = users.Select(u => u.ToAdminListDto(adminIds.Contains(u.Id))).ToList();
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