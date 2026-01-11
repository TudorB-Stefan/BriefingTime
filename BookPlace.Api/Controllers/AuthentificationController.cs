using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class AuthentificationController(AppDbContext context,UserManager<User> userManager) : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] RegisterDto registerDto)
    {
        var found = await context.Users.FindAsync("1");
        if (found == null) return Unauthorized();
        return found;
    }
}