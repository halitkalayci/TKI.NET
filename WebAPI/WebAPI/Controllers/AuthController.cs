using Business.Abstracts;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Entities.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ILogger = Serilog.ILogger;

namespace WebAPI.Controllers
{
    // www.api.tki.com/api/auth HTTP GET REQUEST
    // 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
          
            return Ok(_authService.Login(email, password));
        }
        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            return Ok(_authService.Register(email, password));
        }
        [HttpPost("request-reset-password")]
        public IActionResult RequestResetPassword(string email) 
        {
            return Ok(_authService.RequestResetPassword(email));
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string email,string token, string newPassword)
        {
            return Ok(_authService.ResetPassword(email, token, newPassword));
        }
    }
}
