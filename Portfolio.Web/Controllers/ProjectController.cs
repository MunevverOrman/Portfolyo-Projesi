using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Web.Entities;



namespace Portfolio.Web.Controllers
{
    public class ProjectController(PortfolioContext context) : Controller
    {
        private void CategoryDropDown()
        {
            var categories = context.Categories.ToList();
            ViewBag.categories = (from x in categories
                                  select new SelectListItem
                                  {
                                      Text = x.CategoryName,
                                      Value = x.CategoryId.ToString()
                                  }).ToList();
        }
        public IActionResult Index()
        {
            //Eager Loading
            var projects = context.Projects.Include(x => x.Category).ToList();

            //lazy loading

            return View(projects);
        }
        public IActionResult CreateProject()
        {
            CategoryDropDown();
            return View();
        }
        [HttpPost]


        public IActionResult CreateProject(Project model)
        {
            CategoryDropDown();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            context.Projects.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index");


        }
        public IActionResult UpdateProject(int id)
        {
            CategoryDropDown();
            var project = context.Projects.Find(id);
            return View(project);
        }
        [HttpPost]
        public IActionResult UpdateProject(Project model)
        {
            CategoryDropDown();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            context.Projects.Update(model);
            context.SaveChanges();
            return RedirectToAction("Index");


        }

        public IActionResult DeleteProject(int id)
        {
            var project = context.Projects.Find(id);
            context.Remove(project);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}

