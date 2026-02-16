using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces;
using BookPlace.Infrastructure.Data;
using CloudMight.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class AuthentificationController(AppDbContext context,UserManager<User> userManager,ITokenService tokenService) : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var existingEmail = await userManager.FindByEmailAsync(registerDto.Email);
        if(existingEmail != null)
        {
            return BadRequest("Email taken!");   
        }
        var existingUsername = await userManager.FindByNameAsync(registerDto.UserName);
        if(existingUsername != null)
        {
            return BadRequest("Username taken!");   
        }
        var user = new User
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };
        var result = await userManager.CreateAsync(user,registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors});
        }
        return Ok();
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if(user == null) return Unauthorized("Invalid email or password.");
        
        var res = await userManager.CheckPasswordAsync(user,loginDto.Password);
        if (!res) return Unauthorized("Invalid email or password.");
        
        var refreshToken = tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(2);
        await userManager.UpdateAsync(user);
        
        return Ok(await user.ToDto(tokenService,refreshToken));
    }

}