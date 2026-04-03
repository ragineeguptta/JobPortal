using JobPortal.Core.Entities;
using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobPortal.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var token = await _authService.LoginAsync(model);

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            // ✅ Save token in session
            HttpContext.Session.SetString("JWToken", token);

            // 🔥 Decode JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // 🔍 Extract claims
            var userId = jwtToken.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid")
                ?.Value;

            var role = jwtToken.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)
                ?.Value;

            var email = jwtToken.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Name)
                ?.Value;

            // ❗ Safety check
            if (userId == null)
            {
                ViewBag.Error = "Invalid token (UserId missing)";
                return View();
            }

            // ✅ Create claims (IMPORTANT)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId), // ✅ REQUIRED
        new Claim(ClaimTypes.Name, email ?? model.Email),
        new Claim(ClaimTypes.Role, role ?? "User"),
        new Claim("JWToken", token) // optional
    };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction("Index", "Job");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var token = await _authService.RegisterAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            // ✅ Save token
            HttpContext.Session.SetString("JWToken", token);

            // ✅ Cookie Auth
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email)
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Job");
        }
        public async Task<IActionResult> Logout()
        {
            // Remove cookie authentication
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear session (JWT)
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToAction("Login", "Auth");
        }
    }
}