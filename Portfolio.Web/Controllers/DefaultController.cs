using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Portfolio.Web.Entities;


namespace Portfolio.Web.Controllers;

[AllowAnonymous]
public class DefaultController(PortfolioContext context) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(UserMessage message)
    {
        context.UserMessages.Add(message);
        await context.SaveChangesAsync();  
        await Task.Delay(1000);
        return RedirectToAction("Index");
    }

}  