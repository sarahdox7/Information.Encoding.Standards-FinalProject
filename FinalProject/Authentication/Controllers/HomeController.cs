using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }

        // ------------ Creating User ------------
        public IActionResult Authenticate()
        {
            var huxClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Hux"),
                new Claim(ClaimTypes.Email, "hux20@mail.com"),
                new Claim("hux.Says", "Sarah is the best aunt!"),
                new Claim(ClaimTypes.Role, "Admin")

            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Huxley"),
                new Claim("DrivingLicense", "A+"),

            };

            var huxIdentity = new ClaimsIdentity(huxClaims, "Huxley Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { huxIdentity, licenseIdentity });

            // -------------- Signing In ----------------
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
