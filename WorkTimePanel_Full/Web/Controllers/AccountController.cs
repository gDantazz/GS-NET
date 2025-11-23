using Microsoft.AspNetCore.Mvc;
using WorkTimePanelFull.Application.DTOs;
using WorkTimePanelFull.Application.Services;

namespace WorkTimePanelFull.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;
        public AccountController(IAuthService auth) { _auth = auth; }

        [HttpGet]
        public IActionResult Login() => View(new UserLoginDto());

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var token = await _auth.AuthenticateAsync(dto);
            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos");
                return View(dto);
            }
            Response.Cookies.Append("access_token", token, new CookieOptions { HttpOnly = true, Secure = false, SameSite = SameSiteMode.Lax });
            return RedirectToAction("Index","Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return RedirectToAction("Login");
        }
    }
}
