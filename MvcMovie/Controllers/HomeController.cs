using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using MvcMovie.Models;

namespace MvcMovie.Controllers;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

public class HomeController : Controller
{
    private readonly  MvcMovieContext _context;

    public HomeController( MvcMovieContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Action to initiate Google login
    public IActionResult Login()
    {
        var redirectUrl = Url.Action("GoogleLoginCallback", "Home");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    // Callback action after Google login
    public async Task<IActionResult> GoogleLoginCallback()
    {
        // Authenticate the user with Google
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
        {
            return RedirectToAction("Index"); // Redirect to home if authentication fails
        }

        // Extract user information from the Google authentication result
        var googleId = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;

        // Check if the user already exists in the database
        var user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId);
        if (user == null)
        {
            // Save new user to the database
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8601 // Possible null reference assignment.
            user = new User
            {
                GoogleId = googleId,
                Email = email,
                Name = name
            };
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8601 // Possible null reference assignment.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Sign in the user to the application
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8604 // Possible null reference argument.
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return RedirectToAction("Index");
    }

    // Action to log out the user
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index");
    }
}