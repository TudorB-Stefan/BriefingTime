using System.Security.Claims;
using BriefingTime.Api.Extensions;
using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;
using BriefingTime.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BriefingTime.Api.Controllers;

[Authorize]
public class UserController(UserManager<User> userManager) : BaseController
{
    [HttpGet("me")]
    public async Task<ActionResult<SelfDto>> GetMyProfile()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        return Ok(user.ToSelfDto());
    }

    [HttpPut("edit-me")]
    public async Task<ActionResult> EditMyProfile([FromBody]SelfUpdateDto selfUpdateDto)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        user.Email = selfUpdateDto.Email ?? user.Email;
        user.UserName = selfUpdateDto.UserName ?? user.UserName;
        user.FirstName = selfUpdateDto.FirstName ?? user.FirstName;
        user.LastName = selfUpdateDto.LastName ?? user.LastName;
        user.ModifiedAt = DateTime.UtcNow;
        var res = await userManager.UpdateAsync(user);
        if (!res.Succeeded)
            return BadRequest(res.Errors.Select(e => e.Description));
        return Ok();
    }

    [HttpDelete("delete-me")]
    public async Task<ActionResult> DeleteMyProfile()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        var res = await userManager.DeleteAsync(user);
        if(!res.Succeeded)
            return BadRequest(res.Errors.Select(e => e.Description));
        return Ok();
    }

    [HttpPut("change-password")]
    public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null) return NotFound();
        var res = await userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword,
            changePasswordDto.Password);
        if(!res.Succeeded)
            return BadRequest(res.Errors.Select(e => e.Description));
        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;
        await userManager.UpdateAsync(user);
        return Ok();
    }
}