using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IISProject.Api.BL.Facades;
using IISProject.Api.BL.Models.Auth;
using IISProject.Api.BL.Models.User;
using IISProject.Api.DAL.Seeds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IISProject.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly UserFacade _userFacade;
    private readonly IConfiguration _configuration;

    public AuthController(UserFacade userFacade, IConfiguration configuration)
    {
        _userFacade = userFacade;
        _configuration = configuration;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterModel registerModel)
    {
        var user = await _userFacade.RegisterAsync(registerModel);
        if (user == null)
        {
            return BadRequest();
        }
        
        user.RoleName = RoleSeeds.UserRole.Name;
        string token = GenerateJwtToken(user);

        // Create a session cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append("jwt", token, cookieOptions);
        return Ok(new {Token = token});
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
        return Ok(new {Token = token});
    }
    
    private string GenerateJwtToken(UserDetailModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]!);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.RoleName)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(6), // Or any time frame you see fit
            Issuer = _configuration["JwtConfig:Issuer"],
            Audience = _configuration["JwtConfig:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    [HttpPost("logout")]
    public ActionResult Logout()
    {
        // Delete the session cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Delete("jwt", cookieOptions);
        return Ok();
    }
    
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        
        var user = await _userFacade.GetByIdAsync(Guid.Parse(userId!));
        
        if (user == null)
        {
            return Unauthorized();
        }

        if (user.RoleName != role)
        {
            return Unauthorized();
        }
        

        var userInfo = new
        {
            UserId = userId,
            Username = username,
            Role = role // or detailedRoles
        };

        return Ok(userInfo);
    }
}