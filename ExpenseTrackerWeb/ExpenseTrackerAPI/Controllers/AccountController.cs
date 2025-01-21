using ExpenseTracker.Application.Services;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AccountController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try 
            {
                var registerResult = await _authService.Register(model);

                if (!registerResult.Succeeded)
                {
                    return BadRequest(registerResult.Errors);
                }

                return Ok("User registered successfully");
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var loginResult = await _authService.Login(model);

                if (String.IsNullOrEmpty(loginResult))
                    return Unauthorized();
                else
                    return Ok(new { Token = loginResult });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
