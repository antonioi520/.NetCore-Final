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
    public class InspectionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            InspectionViewModel inspectionVm = new InspectionViewModel();
            using (var db = new InspectionDBContext())
            {
                inspectionVm.InspectionList = db.Inspections.ToList();
                inspectionVm.NewInspection = new Inspection();
                inspectionVm.Inspectors = GetInspectorDDL();
                inspectionVm.Bridges = GetBridgeDDL();
                inspectionVm.SubstructureInspectionCodes = GetSubstructureCodeDDL();
                inspectionVm.SuperstructureInspectionCodes = GetSuperstructureCodeDDL();
                inspectionVm.DeckInspectionCodes = GetDeckInspectionCodeDDL();
            }
            return View(inspectionVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(InspectionViewModel inspectionVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new InspectionDBContext())
                {
                    db.Inspections.Add(inspectionVm.NewInspection);
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
            InspectionViewModel inspectionVm = new InspectionViewModel();
            using (InspectionDBContext db = new InspectionDBContext())
            {
                //assign view model object to value of current object id passed in route
                inspectionVm.NewInspection = db.Inspections.Where(
                    e => e.InspectionId == id).SingleOrDefault();
                //get  lists
                inspectionVm.Inspectors = GetInspectorDDL();
                inspectionVm.Bridges = GetBridgeDDL();
                inspectionVm.SubstructureInspectionCodes = GetSubstructureCodeDDL();
                inspectionVm.SuperstructureInspectionCodes = GetSuperstructureCodeDDL();
                inspectionVm.DeckInspectionCodes = GetDeckInspectionCodeDDL();
                //return the view model
                return View(inspectionVm);
            }
        }

        [HttpPost]
        public IActionResult Edit(InspectionViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (InspectionDBContext db = new InspectionDBContext())
                {
                    //instantiate object from view model
                    Inspection e = obj.NewInspection;
                    //retrieve primary key/id from route data
                    e.InspectionId = Guid.Parse(RouteData.Values["id"].ToString());
                    //mark the record as modified
                    db.Entry(e).State = EntityState.Modified;
                    //persist changes
                    db.SaveChanges();
                }
            }
            //return to main controller/action
            return RedirectToAction("Index");
        }
        private static List<SelectListItem> GetBridgeDDL()
        {
            List<SelectListItem> bridgelist = new List<SelectListItem>();
            BridgeViewModel bridgevm = new BridgeViewModel();
            using (var bridgedb = new BridgeDBContext())
            {
                bridgevm.BridgeList = bridgedb.Bridges.ToList();
            }
            foreach (Bridge bridge in bridgevm.BridgeList)
            {
                bridgelist.Add(new SelectListItem
                {
                    Text = bridge.Location,
                    Value = bridge.BridgeId.ToString()
                });
            }
            return bridgelist;
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

        private static List<SelectListItem> GetDeckInspectionCodeDDL()
        {
            List<SelectListItem> deckinspectionlist = new List<SelectListItem>();
            InspectionViewModel deckinspectionvm = new InspectionViewModel();
            using (var deckinspectiondb = new InspectionDBContext())
            {
                deckinspectionvm.InspectionList = deckinspectiondb.Inspections.ToList();
            }
            foreach (Inspection deckinspection in deckinspectionvm.InspectionList)
            {
                deckinspectionlist.Add(new SelectListItem
                {
                    Text = deckinspection.DeckInspectionCodeId.ToString(),
                    Value = deckinspection.DeckInspectionCodeId.ToString()
                });
            }
            return deckinspectionlist;
        }

        private static List<SelectListItem> GetSuperstructureCodeDDL()
        {
            List<SelectListItem> superstructurelist = new List<SelectListItem>();
            InspectionViewModel superstructurevm = new InspectionViewModel();
            using (var superstructuredb = new InspectionDBContext())
            {
                superstructurevm.InspectionList = superstructuredb.Inspections.ToList();
            }
            foreach (Inspection superstructure in superstructurevm.InspectionList)
            {
                superstructurelist.Add(new SelectListItem
                {
                    Text = superstructure.SuperstructureInspectionCodeId.ToString(),
                    Value = superstructure.SuperstructureInspectionCodeId.ToString()
                });
            }
            return superstructurelist;
        }

        private static List<SelectListItem> GetSubstructureCodeDDL()
        {
            List<SelectListItem> substructurelist = new List<SelectListItem>();
            InspectionViewModel substructurevm = new InspectionViewModel();
            using (var substructuredb = new InspectionDBContext())
            {
                substructurevm.InspectionList = substructuredb.Inspections.ToList();
            }
            foreach (Inspection substructure in substructurevm.InspectionList)
            {
                substructurelist.Add(new SelectListItem
                {
                    Text = substructure.SubstructureInspectionCodeId.ToString(),
                    Value = substructure.SubstructureInspectionCodeId.ToString()
                });
            }
            return substructurelist;
        }

    }
}
