using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Auth;
using IISProject.Api.BL.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly UserFacade _userFacade;
    private readonly IConfiguration _configuration;
    private readonly RoleOfUserFacade _roleOfUserFacade;

    public AuthController(UserFacade userFacade, IConfiguration configuration, RoleOfUserFacade roleOfUserFacade)
    {
        _userFacade = userFacade;
        _configuration = configuration;
        _roleOfUserFacade = roleOfUserFacade;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginModel loginModel)
    {
        var user = await _userFacade.AuthenticateAsync(loginModel.Username, loginModel.Password);
        if (user == null)
        {
            return Unauthorized();
        }
        
        string token = GenerateJwtToken(user);

        // Create a session cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append("jwt", token, cookieOptions);
        return Ok(new { Token = token });
    }
    
    private string GenerateJwtToken(UserDetailModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
            // Add more claims as needed
        };

        // Add role claims
        var userRoles = _roleOfUserFacade.GetByUserIdAsync(user.Id);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(6), // Or any time frame you see fit
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}