using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("/api")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(IConfiguration configuration, AuthService authService, UserService userService)
        {
            _configuration = configuration;
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("/login")]
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

        [HttpPost("/register")]
        public IActionResult Register([FromBody] User newUser)
        {
            try
            {
                _userService.CreateUser(newUser.Nickname, newUser.Password);
                return Ok("Utente creato con successo.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la creazione dell'utente: {ex.Message}");
            }
        }

        [HttpGet("/users")]
        public IActionResult ListUsers()
        {
            var users = _userService.ListUsers();
            return Ok(users);
        }

        [HttpDelete("/users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var userToDelete = _userService.GetUserById(id);
                if(userToDelete == null)
                {
                    return NotFound($"L'utente non è stato trovato");
                }
                _userService.DeleteUser(id);
                return Ok($"Utente {id} eliminato con successo.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante l'eliminazione dell'utente: {ex.Message}");
            }
        }

        [HttpPut("users/{id}/changepassword")]
        public IActionResult ChangePassword(int id, [FromBody] string newPassword)
        {
            try
            {
                _userService.ChangePassword(id, newPassword);
                return Ok($"Password dell'utente {id} modificata con successo.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante la modifica della password dell'utente: {ex.Message}");
            }
        }
    }
}