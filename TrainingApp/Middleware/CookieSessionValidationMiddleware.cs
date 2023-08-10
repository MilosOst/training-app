using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrainingApp.APIResponses;
using TrainingApp.Data;

namespace TrainingApp.Middleware;

public class CookieSessionValidationMiddleware: CookieAuthenticationEvents
{
    private readonly DataContext _db;

    public CookieSessionValidationMiddleware(DataContext db)
    {
        _db = db;
    }

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var sessionId = context.Principal.Claims.FirstOrDefault(p => p.Type == "sessionId");
        if (sessionId is null)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return;
        }
        
        // Verify in database that sessionId is valid
        var session = await _db.UserSessions.FirstOrDefaultAsync(p => p.SessionId.ToString() == sessionId.Value);
        if (session is null)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return;
        }
        
        var userIdClaim = new Claim(ClaimTypes.NameIdentifier, session.UserId.ToString());
        var sessionIdClaim = new Claim("SessionId", session.SessionId.ToString());
        
        ((ClaimsIdentity) (context.Principal.Identity)).AddClaim(userIdClaim);
        ((ClaimsIdentity) (context.Principal.Identity)).AddClaim(sessionIdClaim);
    }
    
    public override async Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        
        MessageResponse msg = new () { Message = "You are unauthorized. Please log in" };
        string json = JsonSerializer.Serialize(msg);
        await context.Response.WriteAsync(json);
    }
}