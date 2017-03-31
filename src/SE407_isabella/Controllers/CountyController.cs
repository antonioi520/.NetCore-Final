using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class CountyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            CountyViewModel countyVm = new CountyViewModel();
            using (var db = new CountyDBContext())
            {
                countyVm.CountyList = db.Counties.ToList();
                countyVm.NewCounty = new County();
            }
            return View(countyVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(CountyViewModel countyVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CountyDBContext())
                {
                    db.Counties.Add(countyVm.NewCounty);
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
            CountyViewModel countyVm = new CountyViewModel();
            using (CountyDBContext db = new CountyDBContext())
            {
                //assign view model object to value of current object id passed in route
                countyVm.NewCounty = db.Counties.Where(
                    e => e.CountyId == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(countyVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(CountyViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (CountyDBContext db = new CountyDBContext())
                {
                    //instantiate object from view model
                    County e = obj.NewCounty;
                    //retrieve primary key/id from route data
                    e.CountyId = Guid.Parse(RouteData.Values["id"].ToString());
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
            CountyViewModel countyVm = new CountyViewModel();
            using (CountyDBContext db = new CountyDBContext())
            {
                //instantiate object from view model
                countyVm.NewCounty = new County();
                //retrieve primary key/id from route data
                countyVm.NewCounty.CountyId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(countyVm.NewCounty).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
