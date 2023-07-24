using Microsoft.AspNetCore.Mvc;
using TrainingApp.Features.Users;

namespace TrainingApp.Features.Authentication;

[Route("auth")]
[ApiController]
public class AuthenticationController: ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest req)
    {
        await _authenticationService.RegisterUser(req);
        return NoContent();
    }
}