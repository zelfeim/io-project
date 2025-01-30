using System.Security.Claims;
using Application.Features.Employee.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly IEmployeeValidationService _employeeValidationService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger,
        IEmployeeValidationService employeeValidationService)
    {
        _logger = logger;
        _employeeValidationService = employeeValidationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!_employeeValidationService.ValidateCredentials(request.Email, request.Password)) return Unauthorized();

        var role = _employeeValidationService.GetRole(request.Email);
        var id = _employeeValidationService.GetId(request.Email);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, request.Email),
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Role, role.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        _logger.LogInformation("User {Email} logged in at {Time}", request.Email, DateTime.Now);

        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        _logger.LogInformation("User {Email} logged out at {Time}", User.FindFirst(ClaimTypes.Name)!.Value,
            DateTime.Now);

        return Ok();
    }

    [HttpGet("roles")]
    [Authorize]
    public ActionResult<string> GetRole()
    {
        var roles = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role);

        return Ok(roles.Value);
    }
}