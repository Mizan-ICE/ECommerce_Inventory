using AutoMapper;
using ECommerce_Inventory.Application.Dtos.Auth;
using ECommerce_Inventory.Domain.Identity;
using ECommerce_Inventory.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce_Inventory.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly IUserStore<Users> _userStore;
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
 

    public AuthController(
        SignInManager<Users> signInManager,
        UserManager<Users> userManager,
        IUserStore<Users> userStore,
        AppDbContext context,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _userStore = userStore;
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            
            return BadRequest(ModelState);

        if (registerDto.Password != registerDto.ConfirmPassword)
           
            return BadRequest("Password and confirmPassword do not match.");

        var existing = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existing != null)
           
            return BadRequest("this email address is already in use");

         var newUser = new Users()
        {
            FullName = registerDto.FullName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

      
        newUser.UserName ??= registerDto.Email.Trim();
        newUser.Email = registerDto.Email.Trim();

        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _signInManager.SignInAsync(newUser, isPersistent: false);
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            return BadRequest("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            return BadRequest("Invalid email or password.");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim("UserId", user.Id)
        };

       
        var accessToken = await _tokenService.GetJwtToken(
            claims,
            _configuration["Jwt:Key"]!,
            _configuration["Jwt:Issuer"]!,
            _configuration["Jwt:Audience"]!
        );

        var refreshToken = GenerateRefreshToken();
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown"
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            UserId = user.Id,
            Email = user.Email!,
            FullName = user.FullName
        };

        return Ok(response);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
