using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class FunctionalClassController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            FunctionalClassViewModel functionalClassVm = new FunctionalClassViewModel();
            using (var db = new FunctionalClassDBContext())
            {
                functionalClassVm.FunctionalClassList = db.FunctionalClasses.ToList();
                functionalClassVm.NewFunctionalClass = new FunctionalClass();
            }
            return View(functionalClassVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(FunctionalClassViewModel functionalClassVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FunctionalClassDBContext())
                {
                    db.FunctionalClasses.Add(functionalClassVm.NewFunctionalClass);
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
            FunctionalClassViewModel functionalClassVm = new FunctionalClassViewModel();
            using (FunctionalClassDBContext db = new FunctionalClassDBContext())
            {
                //assign view model object to value of current object id passed in route
                functionalClassVm.NewFunctionalClass = db.FunctionalClasses.Where(
                    e => e.FunctionalClassId == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(functionalClassVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(FunctionalClassViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (FunctionalClassDBContext db = new FunctionalClassDBContext())
                {
                    //instantiate object from view model
                    FunctionalClass e = obj.NewFunctionalClass;
                    //retrieve primary key/id from route data
                    e.FunctionalClassId = Guid.Parse(RouteData.Values["id"].ToString());
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
            FunctionalClassViewModel functionalClassVm = new FunctionalClassViewModel();
            using (FunctionalClassDBContext db = new FunctionalClassDBContext())
            {
                //instantiate object from view model
                functionalClassVm.NewFunctionalClass = new FunctionalClass();
                //retrieve primary key/id from route data
                functionalClassVm.NewFunctionalClass.FunctionalClassId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(functionalClassVm.NewFunctionalClass).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
