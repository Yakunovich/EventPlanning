using Microsoft.AspNetCore.Mvc;

namespace EventPlanningBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _accountService.RegisterAsync(registerDto);
            if(result == "Email already registered")
            {
                return BadRequest("Email already registered");
            }

            return Ok(result);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Token and email are required for email confirmation.");
            }

            try
            {
                var result = await _accountService.ConfirmEmailAsync(email, token);
                if (result)
                {
                    return Ok("Email confirmed successfully.");
                }
                else
                {
                    return BadRequest("Email confirmation failed.");
                }
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginDto loginDto)
        {
            var token = await _accountService.AuthenticateAsync(loginDto);
            if (token == null)
            {
                return Unauthorized("Invalid credentials");
            }

            if (token == "Email not confirmed")
            {
                return BadRequest("Email not confirmed. Please confirm your email.");
            }

            return Ok(new { Token = token });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var profile = await _accountService.GetProfileAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
    }
}
