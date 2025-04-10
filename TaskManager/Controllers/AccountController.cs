using System.Security.Claims;
using System.Text.Json;
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

        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
        }

        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        // 🟡 Fetch profile picture manually using access token
        var accessToken = result.Properties?.GetTokenValue("access_token");
        string? picture = null;

        if (!string.IsNullOrEmpty(accessToken))
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetStringAsync("https://www.googleapis.com/oauth2/v2/userinfo");
            var userInfo = JsonDocument.Parse(response);
            picture = userInfo.RootElement.GetProperty("picture").GetString();
        }

        Console.WriteLine($"User signed in: {name} ({email})");
        Console.WriteLine($"User Picture: {picture}");
        // Redirect back to frontend
        return Redirect($"http://localhost:3000/auth/callback?email={email}&name={name}&picture={picture}");
    }


    [HttpGet("/Account/Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Console.WriteLine("User logged out");
        return Redirect("http://localhost:3000");
    }
}