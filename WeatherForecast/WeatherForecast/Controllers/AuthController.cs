using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WeatherApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;

        public AuthController(IConfiguration configuration, AuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User request)
        {
            string adminUsername = _configuration["AdminCredentials:Nickname"];
            string adminPassword = _configuration["AdminCredentials:Password"];

            if (request.Nickname == adminUsername && request.Password == adminPassword)
            {
                return Ok("Accesso consentito come admin.");
            }
            else
            {
                return Unauthorized("Credenziali non valide.");
            }
        }
    }
}