using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;
using System.Linq;

namespace Portfolio.Web.Controllers
{
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;

       
        public SettingController(PortfolioContext context)
        {
            _context = context;
        }

    
        public IActionResult ListUser()
        {
            var userList = _context.Users.ToList();
            return View(userList);
        }

 
        public IActionResult PasswordUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return View(user); 
        }

  
        [HttpPost]
        public IActionResult PasswordUser(int id, string password)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            if (user.Password == password)
            {
        
                return RedirectToAction("UpdateUser", new { id = user.UserId });
            }

            ModelState.AddModelError("", "Şifre yanlış!");
            return View(user);
        }

     
        public IActionResult UpdateUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return View(user);
        }


        [HttpPost]
        public IActionResult UpdateUser(User model, string currentPassword, string newPassword, string confirmPassword)
        {
            var existingUser = _context.Users.Find(model.UserId);
            if (existingUser == null)
                return NotFound();

            existingUser.FirstName = model.FirstName;
            existingUser.Lastname = model.Lastname;
            existingUser.UserName = model.UserName;

   
            if (!string.IsNullOrEmpty(currentPassword)
                || !string.IsNullOrEmpty(newPassword)
                || !string.IsNullOrEmpty(confirmPassword))
            {
              
                if (existingUser.Password != currentPassword)
                {
                    ModelState.AddModelError("PasswordError", "Mevcut şifre yanlış!");
                    return View(model);
                }

                if (newPassword != confirmPassword)
                {
                    ModelState.AddModelError("PasswordError", "Yeni şifre ve onayı eşleşmiyor!");
                    return View(model);
                }

                existingUser.Password = newPassword;
            }

            _context.SaveChanges();
            return RedirectToAction("ListUser");
        }

    }
}
