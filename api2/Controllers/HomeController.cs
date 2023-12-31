using Microsoft.AspNetCore.Mvc;

namespace api_route.Controllers;

[ApiController]
[Route("api2/[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("ping")]
    public ActionResult<string> Ping()
    {
        _logger.LogInformation("Ejecutando ping en API2");
        return Ok("pong2");
    }
}
