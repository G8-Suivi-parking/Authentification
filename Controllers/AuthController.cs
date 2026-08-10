
using Microsoft.AspNetCore.Mvc;
using BimaTech.Parking.Auth;

namespace BimaTech.Parking.Controllers;

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
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest? request)
    {
        if (request is null)
        {
            return BadRequest(
                new ErrorResponse("Les données de connexion sont requises.")
            );
        }

        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(
                new ErrorResponse("Email et mot de passe sont requis.")
            );
        }

        var result = await _authService.LoginAsync(
            request.Email,
            request.Password
        );

        if (result is null)
        {
            return Unauthorized(
                new ErrorResponse("Email ou mot de passe incorrect.")
            );
        }

        return Ok(result);
    }
}

