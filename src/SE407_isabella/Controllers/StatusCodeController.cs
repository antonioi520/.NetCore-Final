using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class StatusCodeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            StatusCodeViewModel statusCodeVm = new StatusCodeViewModel();
            using (var db = new StatusCodeDBContext())
            {
                statusCodeVm.StatusCodeList = db.StatusCodes.ToList();
                statusCodeVm.NewStatusCode = new StatusCode();
            }
            return View(statusCodeVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(StatusCodeViewModel statusCodeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new StatusCodeDBContext())
                {
                    db.StatusCodes.Add(statusCodeVm.NewStatusCode);
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
            StatusCodeViewModel statusCodeVm = new StatusCodeViewModel();
            using (StatusCodeDBContext db = new StatusCodeDBContext())
            {
                //assign view model object to value of current object id passed in route
                statusCodeVm.NewStatusCode = db.StatusCodes.Where(
                    e => e.StatusCodeId == id).SingleOrDefault();
                //get departments for select list
           
                //return the view model
                return View(statusCodeVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(StatusCodeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (StatusCodeDBContext db = new StatusCodeDBContext())
                {
                    //instantiate object from view model
                    StatusCode e = obj.NewStatusCode;
                    //retrieve primary key/id from route data
                    e.StatusCodeId = Guid.Parse(RouteData.Values["id"].ToString());
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
            StatusCodeViewModel statusCodeVm = new StatusCodeViewModel();
            using (StatusCodeDBContext db = new StatusCodeDBContext())
            {
                //instantiate object from view model
                statusCodeVm.NewStatusCode = new StatusCode();
                //retrieve primary key/id from route data
                statusCodeVm.NewStatusCode.StatusCodeId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(statusCodeVm.NewStatusCode).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
