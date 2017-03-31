using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class ConstructionDesignController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ConstructionDesignViewModel constructionDesignVm = new ConstructionDesignViewModel();
            using (var db = new ConstructionDesignDBContext())
            {
                constructionDesignVm.ConstructionDesignList = db.ConstructionDesigns.ToList();
                constructionDesignVm.NewConstructionDesign = new ConstructionDesign();
            }
            return View(constructionDesignVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(ConstructionDesignViewModel constructionDesignVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ConstructionDesignDBContext())
                {
                    db.ConstructionDesigns.Add(constructionDesignVm.NewConstructionDesign);
                    db.SaveChanges();
                    //Redirect to get Index GET method
                    return RedirectToAction("Index");
                }
            }
            //Redirect to get Index GET method
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            ConstructionDesignViewModel constructionDesignVm = new ConstructionDesignViewModel();
            using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
            {
                //assign view model object to value of current object id passed in route
                constructionDesignVm.NewConstructionDesign = db.ConstructionDesigns.Where(
                    e => e.ConstructionDesignId == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(constructionDesignVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(ConstructionDesignViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
                {
                    //instantiate object from view model
                    ConstructionDesign e = obj.NewConstructionDesign;
                    //retrieve primary key/id from route data
                    e.ConstructionDesignId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(e).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }

        //GET - DELETE OPERATION
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            ConstructionDesignViewModel constructionDesignVm = new ConstructionDesignViewModel();
            using (ConstructionDesignDBContext db = new ConstructionDesignDBContext())
            {
                //instantiate object from view model
                constructionDesignVm.NewConstructionDesign = new ConstructionDesign();
                //retrieve primary key/id from route data
                constructionDesignVm.NewConstructionDesign.ConstructionDesignId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(constructionDesignVm.NewConstructionDesign).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
