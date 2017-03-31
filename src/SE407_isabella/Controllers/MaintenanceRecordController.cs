using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class MaintenanceRecordController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            MaintenanceRecordViewModel maintenanceRecordVm = new MaintenanceRecordViewModel();
            using (var db = new MaintenanceRecordDBContext())
            {
                maintenanceRecordVm.MaintenanceRecordList = db.MaintenanceRecords.ToList();
                maintenanceRecordVm.NewMaintenanceRecord = new MaintenanceRecord();
                maintenanceRecordVm.Inspectors = GetInspectorDDL();
                maintenanceRecordVm.MaintenanceActions = GetMaintenanceActionDDL();
                
            }
            return View(maintenanceRecordVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(MaintenanceRecordViewModel maintenanceRecordVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaintenanceRecordDBContext())
                {
                    db.MaintenanceRecords.Add(maintenanceRecordVm.NewMaintenanceRecord);
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
            MaintenanceRecordViewModel maintenanceRecordVm = new MaintenanceRecordViewModel();
            using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
            {
                //assign view model object to value of current object id passed in route
                maintenanceRecordVm.NewMaintenanceRecord = db.MaintenanceRecords.Where(
                    e => e.MaintenanceRecordId == id).SingleOrDefault();
                //get  lists
                maintenanceRecordVm.Inspectors = GetInspectorDDL();
                maintenanceRecordVm.MaintenanceActions = GetMaintenanceActionDDL();
                //return the view model
                return View(maintenanceRecordVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(MaintenanceRecordViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (MaintenanceRecordDBContext db = new MaintenanceRecordDBContext())
                {
                    //instantiate object from view model
                    MaintenanceRecord e = obj.NewMaintenanceRecord;
                    //retrieve primary key/id from route data
                    e.MaintenanceRecordId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(e).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
        private static List<SelectListItem> GetMaintenanceActionDDL()
        {
            List<SelectListItem> maintenanceactionlist = new List<SelectListItem>();
            MaintenanceActionViewModel maintenanceactionvm = new MaintenanceActionViewModel();
            using (var maintenanceactiondb = new MaintenanceActionDBContext())
            {
                maintenanceactionvm.MaintenanceActionList = maintenanceactiondb.MaintenanceActions.ToList();
            }
            foreach (MaintenanceAction maintenanceaction in maintenanceactionvm.MaintenanceActionList)
            {
                maintenanceactionlist.Add(new SelectListItem
                {
                    Text = maintenanceaction.MaintenanceActionName,
                    Value = maintenanceaction.MaintenanceActionId.ToString()
                });
            }
            return maintenanceactionlist;
        }

        private static List<SelectListItem> GetInspectorDDL()
        {
            List<SelectListItem> inspectorlist = new List<SelectListItem>();
            InspectorViewModel inspectorvm = new InspectorViewModel();
            using (var inspectordb = new InspectorDBContext())
            {
                inspectorvm.InspectorList = inspectordb.Inspectors.ToList();
            }
            foreach (Inspector inspector in inspectorvm.InspectorList)
            {
                inspectorlist.Add(new SelectListItem
                {
                    Text = inspector.InspectorOrg,
                    Value = inspector.InspectorId.ToString()
                });
            }
            return inspectorlist;
        }
    }
}
