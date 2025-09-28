using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;
using Portfolio.Web.Models;
using System.Security.Claims;

namespace Portfolio.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController(PortfolioContext context) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Login(LoginViewModel model)
        {
            //Fast Fail : hatayı yakala ve hemen dön 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = context.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);//Girilen değerler var mı yok mu kjontrol
          
            if (user is null)//c# ın güncel sürümünde is sağlanır
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View(model);
            }

            var claims = new List<Claim> // Claim Sisteme gişriş yapan kişinin bilgilerini tutar
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim("FullName",string.Join(" ",user.FirstName,user.Lastname))
            };
            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);//Authentication türü cookie

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc=DateTime.UtcNow.AddMinutes(30),
                IsPersistent = false // tarayıcı kapansa bile oturum açık kalsın mı
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);

           HttpContext.Session.SetString("UserName", user.UserName);
          
            return RedirectToAction("Index", "Statistics");


         
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserName");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }
    }
}
