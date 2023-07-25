using System.Security.Claims;

namespace TrainingApp.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Return the currently authenticated user's userId. Do not use if user is not logged in.
    /// </summary>
    /// <param name="context">Current HttpContext</param>
    /// <returns>userId of currently logged in User</returns>
    public static string GetUserId(this HttpContext context)
    {
        return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
    }
}