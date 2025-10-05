using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs.User;
using TodoApp.Application.Services;

//Presentation Layer görevini görür.
//HTTP isteklerini karşılar, Application katmanındaki servisleri çağırır.
//DTO kullanarak veri alışverişi yapar.
//Doğrudan veritabanı veya domain ile çalışmaz.
//HTTP durum kodlarını (200, 201, 204, 400, 401, 404) doğru şekilde döner.
//UI bu endpoint’ler üzerinden sisteme erişir.

namespace TodoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.CreateAsync(dto);
            return StatusCode(201); // Created
        }

        // POST: api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized("Geçersiz e-posta veya şifre.");

            // İleride JWT token dönebilirsin
            return Ok(user);
        }

        // PUT: api/user/{id}/email
        [HttpPut("{id}/email")]
        public async Task<IActionResult> UpdateEmail(Guid id, [FromBody] string newEmail)
        {
            await _userService.UpdateEmailAsync(id, newEmail);
            return NoContent();
        }

        // PUT: api/user/{id}/username
        [HttpPut("{id}/username")]
        public async Task<IActionResult> UpdateUsername(Guid id, [FromBody] string newUsername)
        {
            await _userService.UpdateUsernameAsync(id, newUsername);
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
