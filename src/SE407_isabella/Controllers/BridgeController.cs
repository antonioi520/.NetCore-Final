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
    public class BridgeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            BridgeViewModel bridgeVm = new BridgeViewModel();
            using (var db = new BridgeDBContext())
            {
                bridgeVm.BridgeList = db.Bridges.ToList();
                bridgeVm.NewBridge = new Bridge();
                bridgeVm.MaterialDesigns = GetMaterialDesignDDL();
                bridgeVm.ConstructionDesigns = GetConstructionDesignDDL();
                bridgeVm.FunctionalClasses = GetFunctionalClassDDL();
                bridgeVm.Statuses = GetStatusDDL();
                bridgeVm.Counties = GetCountyDDL();
            }
            return View(bridgeVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(BridgeViewModel bridgeVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BridgeDBContext())
                {
                    db.Bridges.Add(bridgeVm.NewBridge);
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
            BridgeViewModel bridgeVm = new BridgeViewModel();
            using (BridgeDBContext db = new BridgeDBContext())
            {
                //assign view model object to value of current object id passed in route
                bridgeVm.NewBridge = db.Bridges.Where(
                    e => e.BridgeId == id).SingleOrDefault();
                //get  lists
                bridgeVm.MaterialDesigns = GetMaterialDesignDDL();
                bridgeVm.ConstructionDesigns = GetConstructionDesignDDL();
                bridgeVm.FunctionalClasses = GetFunctionalClassDDL();
                bridgeVm.Statuses = GetStatusDDL();
                bridgeVm.Counties = GetCountyDDL();
                //return the view model
                return View(bridgeVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(BridgeViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (BridgeDBContext db = new BridgeDBContext())
                {
                    //instantiate object from view model
                    Bridge e = obj.NewBridge;
                    //retrieve primary key/id from route data
                    e.BridgeId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(e).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }

        private static List<SelectListItem> GetMaterialDesignDDL()
        {
            List<SelectListItem> materialdesign = new List<SelectListItem>();
            MaterialDesignViewModel matdesignvm = new MaterialDesignViewModel();
            using (var matdesigndb = new MaterialDesignDBContext())
            {
                matdesignvm.MaterialDesignList = matdesigndb.MaterialDesigns.ToList();
            }
            foreach (MaterialDesign matdesign in matdesignvm.MaterialDesignList)
            {
                materialdesign.Add(new SelectListItem
                {
                    Text = matdesign.MaterialDesignType,
                    Value = matdesign.MaterialDesignId.ToString()
                });
            }
            return materialdesign;
        }

        private static List<SelectListItem> GetConstructionDesignDDL()
        {
            List<SelectListItem> constructiondesign = new List<SelectListItem>();
            ConstructionDesignViewModel constructdesignvm = new ConstructionDesignViewModel();
            using (var constructdesigndb = new ConstructionDesignDBContext())
            {
                constructdesignvm.ConstructionDesignList = constructdesigndb.ConstructionDesigns.ToList();
            }
            foreach (ConstructionDesign constructdesign in constructdesignvm.ConstructionDesignList)
            {
                constructiondesign.Add(new SelectListItem
                {
                    Text = constructdesign.ConstructionDesignType,
                    Value = constructdesign.ConstructionDesignId.ToString()
                });
            }
            return constructiondesign;
        }

        private static List<SelectListItem> GetFunctionalClassDDL()
        {
            List<SelectListItem> functionalclass = new List<SelectListItem>();
            FunctionalClassViewModel functclassvm = new FunctionalClassViewModel();
            using (var functclassdb = new FunctionalClassDBContext())
            {
                functclassvm.FunctionalClassList = functclassdb.FunctionalClasses.ToList();
            }
            foreach (FunctionalClass functclass in functclassvm.FunctionalClassList)
            {
                functionalclass.Add(new SelectListItem
                {
                    Text = functclass.FunctionalClassType,
                    Value = functclass.FunctionalClassId.ToString()
                });
            }
            return functionalclass;
        }

        private static List<SelectListItem> GetStatusDDL()
        {
            List<SelectListItem> statuscode = new List<SelectListItem>();
            StatusCodeViewModel statusvm = new StatusCodeViewModel();
            using (var statusdb = new StatusCodeDBContext())
            {
                statusvm.StatusCodeList = statusdb.StatusCodes.ToList();
            }
            foreach (StatusCode status in statusvm.StatusCodeList)
            {
                statuscode.Add(new SelectListItem
                {
                    Text = status.StatusName,
                    Value = status.StatusCodeId.ToString()
                });
            }
            return statuscode;
        }

        private static List<SelectListItem> GetCountyDDL()
        {
            List<SelectListItem> countylist = new List<SelectListItem>();
            CountyViewModel countyvm = new CountyViewModel();
            using (var countydb = new CountyDBContext())
            {
                countyvm.CountyList = countydb.Counties.ToList();
            }
            foreach (County county in countyvm.CountyList)
            {
                countylist.Add(new SelectListItem
                {
                    Text = county.CountyName,
                    Value = county.CountyId.ToString()
                });
            }
            return countylist;
        }
    }
}
