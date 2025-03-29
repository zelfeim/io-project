using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
[AllowAnonymous]
public class TestController : Controller
{
    [HttpGet("test-api")]
    public IActionResult TestGet()
    {
        return Ok("Test api response!");
    }
}