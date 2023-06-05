﻿using Business.Abstracts;
using Entities.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
