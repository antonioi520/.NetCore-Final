using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class InspectorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (var db = new InspectorDBContext())
            {
                inspectorVm.InspectorList = db.Inspectors.ToList();
                inspectorVm.NewInspector = new Inspector();
            }
            return View(inspectorVm);
        }
        //Post
        [HttpPost]
        public IActionResult Index(InspectorViewModel inspectorVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectorDBContext())
                {
                    db.Inspectors.Add(inspectorVm.NewInspector);
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
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (InspectorDBContext db = new InspectorDBContext())
            {
                //assign view model object to value of current object id passed in route
                inspectorVm.NewInspector = db.Inspectors.Where(
                    e => e.InspectorId == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(inspectorVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(InspectorViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (InspectorDBContext db = new InspectorDBContext())
                {
                    //instantiate object from view model
                    Inspector e = obj.NewInspector;
                    //retrieve primary key/id from route data
                    e.InspectorId = Guid.Parse(RouteData.Values["id"].ToString());
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
            InspectorViewModel inspectorVm = new InspectorViewModel();
            using (InspectorDBContext db = new InspectorDBContext())
            {
                //instantiate object from view model
                inspectorVm.NewInspector = new Inspector();
                //retrieve primary key/id from route data
                inspectorVm.NewInspector.InspectorId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(inspectorVm.NewInspector).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
