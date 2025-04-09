using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpGet("/Account/Login")]
    public IActionResult Login()
    {
        var redirectUrl = Url.Action("GoogleResponse", "Account");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }


    [HttpGet("/Account/GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync();
        var claims = result.Principal?.Identities?.FirstOrDefault()?.Claims;

        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        // Just for now, log claims
        Console.WriteLine($"User signed in: {name} ({email})");

        // Redirect back to frontend
        return Redirect($"http://localhost:3000/auth/callback?email={email}&name={name}");
    }

    [HttpGet("/Account/Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Console.WriteLine("User logged out");
        return Redirect("http://localhost:3000");
    }
}