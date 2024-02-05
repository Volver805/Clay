using Clay.Api.Requests;
using Microsoft.AspNetCore.Mvc;
using Clay.Domain.Services;

namespace Clay.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            try
            {
                var token = await _authenticationService.AuthenticateUser(request.username, request.password);
                
                if (token == null)
                {
                    return Unauthorized(new { Error = "Validation Error", Message = "Invalid Username or Password." });
                }

                return Ok(new { Message = "Login Successful." , Token = token});
            } catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Server Error", Message = "Something Went Wrong." });
            }
        }
    }
}
