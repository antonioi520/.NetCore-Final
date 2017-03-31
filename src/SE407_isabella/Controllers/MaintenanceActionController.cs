using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class MaintenanceActionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            MaintenanceActionViewModel maintenanceActionVm = new MaintenanceActionViewModel();
            using (var db = new MaintenanceActionDBContext())
            {
                maintenanceActionVm.MaintenanceActionList = db.MaintenanceActions.ToList();
                maintenanceActionVm.NewMaintenanceAction = new MaintenanceAction();
            }
            return View(maintenanceActionVm);
        }
        //Post
        [HttpPost]
        public IActionResult Index(MaintenanceActionViewModel maintenanceActionVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaintenanceActionDBContext())
                {
                    db.MaintenanceActions.Add(maintenanceActionVm.NewMaintenanceAction);
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
            MaintenanceActionViewModel maintenanceActionVm = new MaintenanceActionViewModel();
            using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
            {
                //assign view model object to value of current object id passed in route
                maintenanceActionVm.NewMaintenanceAction = db.MaintenanceActions.Where(
                    e => e.MaintenanceActionId == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(maintenanceActionVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(MaintenanceActionViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
                {
                    //instantiate object from view model
                    MaintenanceAction e = obj.NewMaintenanceAction;
                    //retrieve primary key/id from route data
                    e.MaintenanceActionId = Guid.Parse(RouteData.Values["id"].ToString());
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
            MaintenanceActionViewModel maintenanceActionVm = new MaintenanceActionViewModel();
            using (MaintenanceActionDBContext db = new MaintenanceActionDBContext())
            {
                //instantiate object from view model
                maintenanceActionVm.NewMaintenanceAction = new MaintenanceAction();
                //retrieve primary key/id from route data
                maintenanceActionVm.NewMaintenanceAction.MaintenanceActionId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(maintenanceActionVm.NewMaintenanceAction).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
