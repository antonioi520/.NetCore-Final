using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class InspectionCodeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            InspectionCodeViewModel inspectionCodeVm = new InspectionCodeViewModel();
            using (var db = new InspectionCodeDBContext())
            {
                inspectionCodeVm.InspectionCodeList = db.InspectionCodes.ToList();
                inspectionCodeVm.NewInspectionCode = new InspectionCode();
            }
            return View(inspectionCodeVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(InspectionCodeViewModel inspectionCodeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectionCodeDBContext())
                {
                    db.InspectionCodes.Add(inspectionCodeVm.NewInspectionCode);
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
            InspectionCodeViewModel inspectionCodeVm = new InspectionCodeViewModel();
            using (InspectionCodeDBContext db = new InspectionCodeDBContext())
            {
                //assign view model object to value of current object id passed in route
                inspectionCodeVm.NewInspectionCode = db.InspectionCodes.Where(
                    e => e.InspectionCodeId == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(inspectionCodeVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(InspectionCodeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (InspectionCodeDBContext db = new InspectionCodeDBContext())
                {
                    //instantiate object from view model
                    InspectionCode e = obj.NewInspectionCode;
                    //retrieve primary key/id from route data
                    e.InspectionCodeId = Guid.Parse(RouteData.Values["id"].ToString());
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
            InspectionCodeViewModel inspectionCodeVm = new InspectionCodeViewModel();
            using (InspectionCodeDBContext db = new InspectionCodeDBContext())
            {
                //instantiate object from view model
                inspectionCodeVm.NewInspectionCode = new InspectionCode();
                //retrieve primary key/id from route data
                inspectionCodeVm.NewInspectionCode.InspectionCodeId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(inspectionCodeVm.NewInspectionCode).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
