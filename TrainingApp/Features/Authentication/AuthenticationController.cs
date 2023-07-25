using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.APIResponses;
using TrainingApp.Features.Authentication.Models;

namespace TrainingApp.Features.Authentication;

[Route("auth")]
[ApiController]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> RegisterUser(RegisterUserRequest req)
    {
        await _authenticationService.RegisterUser(req);
        return NoContent();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(MessageResponse))]
    public async Task<ActionResult<MessageResponse>> LoginUser(LoginUserRequest req)
    {
        string sessionId = await _authenticationService.LoginUser(req.EmailOrUsername, req.Password);
        
        // Attach cookie to request
        var claimsIdentity = new ClaimsIdentity(new[] { new Claim("sessionId", sessionId) },
            CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity)); 
        
        return Ok(new MessageResponse() { Message = "You have successfully logged in." });
    }
}