using JobPortal.UI.Services.Interfaces;
using JobPortal.UI.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
            HttpContext.Session.Remove("JWToken");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}