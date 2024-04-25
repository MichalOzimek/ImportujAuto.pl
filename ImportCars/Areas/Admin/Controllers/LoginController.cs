using ImportCars.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImportCars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            
            if (!_context.Admin.Any())
            {
                return RedirectToAction("Create", "Admin", new { area = "Admin" });
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(Models.Admin admin)
        {
            var result = _context.Admin.Where(x => x.Email == admin.Email && x.Password == admin.Password);
            if (result.Count() != 0)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Auctions");
            }
            else
            {
                ViewData["LoginFlag"] = "Błędny login lub hasło";
                return View();

            }
        }
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}