using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookPlace.Core.Entities;
using BookPlace.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookPlace.Infrastructure.Services;

public class TokenService(IConfiguration config,UserManager<User> userManager) : ITokenService
{
    public async Task<string> CreateToken(User user)
    {
        var tokenKey = config["Jwt:Key"] ?? throw new Exception("TokenKey is missing");
        if (tokenKey.Length < 64)
            throw new Exception("Invalid token key lenght");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role,role)));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = creds,
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

/*
public class TokenService(IConfiguration config, UserManager<User> userManager) : ITokenService // Injects the app settings (config) and database user manager into the service
{
    public async Task<string> CreateToken(User user) // Starts an asynchronous method that takes a User object and promises to return a string (the token)
    {
        var tokenKey = config["Jwt:Key"] ?? throw new Exception("TokenKey is missing"); // Looks in appsettings.json for "Jwt:Key". If it's not there, crashes the app immediately to prevent security flaws.
        
        if (tokenKey.Length < 64) // Checks if the secret key is long enough to be mathematically secure
            throw new Exception("Invalid token key lenght"); // Crashes the app if the key is too short, forcing the developer to use a stronger password.
            
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); // Converts your plain-text secret key into an array of bytes, which the cryptography library requires.
        
        var claims = new List<Claim> // Creates an empty list to hold the user's "ID badge" information (claims).
        {
            new Claim(ClaimTypes.Email, user.Email), // Adds the user's email address to the ID badge.
            new Claim(ClaimTypes.NameIdentifier, user.Id) // Adds the user's unique database ID to the ID badge.
        };
        
        var roles = await userManager.GetRolesAsync(user); // Pauses this method, goes to the database, and asks: "What roles (Admin, Member, etc.) does this user have?"
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role))); // Takes the list of roles from the database, turns each one into a "Role Claim", and adds them all to the ID badge.
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature); // Combines your secret key with the SHA-256 encryption algorithm to create a digital signature stamp.
        
        var tokenDescriptor = new SecurityTokenDescriptor // Creates the official "blueprint" that dictates exactly what the token will look like.
        {
            Subject = new ClaimsIdentity(claims), // Attaches the ID badge (email, ID, roles) we built earlier to this blueprint.
            Expires = DateTime.UtcNow.AddDays(3), // Sets a strict expiration date: exactly 3 days from this exact millisecond.
            SigningCredentials = creds, // Attaches the digital signature stamp we created so the server can prove it minted this token.
            Issuer = config["Jwt:Issuer"], // Reads the appsettings to state WHO created this token (e.g., "https://myapi.com").
            Audience = config["Jwt:Audience"] // Reads the appsettings to state WHO is allowed to use this token (e.g., "https://myfrontend.com").
        };
        
        var tokenHandler = new JwtSecurityTokenHandler(); // Summons the built-in .NET tool whose sole job is to construct and read JWTs.
        
        var token = tokenHandler.CreateToken(tokenDescriptor); // Feeds the blueprint into the tool to generate the actual token object in memory.
        
        return tokenHandler.WriteToken(token); // Takes that token object in memory, translates it into the long, readable Base64 string (eyJh...), and hands it back to whoever called this method.
    }
}
*/