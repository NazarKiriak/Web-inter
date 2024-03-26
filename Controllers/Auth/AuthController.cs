﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTwebAPI.Services;
using RESTwebAPI.Models;

namespace RESTwebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel request)
        {
            var user = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized("Неправильна адреса електронної пошти або пароль.");
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User newUser)
        {
            try
            {
                var user = await _authService.RegisterAsync(newUser);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
