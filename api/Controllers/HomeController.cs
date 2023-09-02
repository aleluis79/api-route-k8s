using Microsoft.AspNetCore.Mvc;

namespace api_route.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    private IHttpClientFactory _httpClientFactory { get; }

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("ping")]
    public ActionResult<string> Ping()
    {
        return Ok("pong");
    }

    [HttpGet("ping2")]
    public ActionResult<string> Ping2() {
        
        var httpClient =  _httpClientFactory.CreateClient("API2");

        var response = httpClient.GetAsync("ping").Result;

        if (response.IsSuccessStatusCode)
        {
            return Ok("pong2");
        } else {
            return Problem("Error - No se pudo conectar a api2!");
        }
    }
}
