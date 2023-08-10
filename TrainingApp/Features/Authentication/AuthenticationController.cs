using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.APIResponses;
using TrainingApp.Extensions;
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

    [HttpGet("session")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    // Use this route to verify session is still valid / persistent login
    public async Task<ActionResult> VerifySession()
    {
        return Ok();
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    public async Task<ActionResult<MessageResponse>> RequestPasswordReset(RequestPasswordResetRequest req)
    {
        await _authenticationService.RequestPasswordReset(req.Email);
        MessageResponse msg = new()
        {
            Message = $"If a matching verified account was found, an email was sent to {req.Email} to allow you to reset your password."
        };
        return Ok(msg);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    public async Task<ActionResult<MessageResponse>> ResetPassword(ResetPasswordRequest req)
    {
        await _authenticationService.ResetPassword(req.Password, req.UserId, req.Token);
        return Ok(new MessageResponse() { Message = "Password reset successfully."});
    }

    [Authorize]
    [HttpPost("log-out")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(MessageResponse))]
    public async Task<ActionResult<MessageResponse>> LogOut()
    {
        await _authenticationService.CloseSession(HttpContext.GetSessionId());
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return Ok(new MessageResponse() { Message = "You have successfully logged out."});
    }
    
    
}