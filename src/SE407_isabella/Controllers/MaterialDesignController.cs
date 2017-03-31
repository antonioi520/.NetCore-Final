using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SE407_isabella.Controllers
{
    public class MaterialDesignController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            MaterialDesignViewModel materialDesignVm = new MaterialDesignViewModel();
            using (var db = new MaterialDesignDBContext())
            {
                materialDesignVm.MaterialDesignList = db.MaterialDesigns.ToList();
                materialDesignVm.NewMaterialDesign = new MaterialDesign();
            }
            return View(materialDesignVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(MaterialDesignViewModel materialDesignVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MaterialDesignDBContext())
                {
                    db.MaterialDesigns.Add(materialDesignVm.NewMaterialDesign);
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
            MaterialDesignViewModel materialDesignVm = new MaterialDesignViewModel();
            using (MaterialDesignDBContext db = new MaterialDesignDBContext())
            {
                //assign view model object to value of current object id passed in route
                materialDesignVm.NewMaterialDesign = db.MaterialDesigns.Where(
                    e => e.MaterialDesignId == id).SingleOrDefault();
                //get departments for select list
                
                //return the view model
                return View(materialDesignVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(MaterialDesignViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (MaterialDesignDBContext db = new MaterialDesignDBContext())
                {
                    //instantiate object from view model
                    MaterialDesign e = obj.NewMaterialDesign;
                    //retrieve primary key/id from route data
                    e.MaterialDesignId = Guid.Parse(RouteData.Values["id"].ToString());
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
            MaterialDesignViewModel materialDesignVm = new MaterialDesignViewModel();
            using (MaterialDesignDBContext db = new MaterialDesignDBContext())
            {
                //instantiate object from view model
                materialDesignVm.NewMaterialDesign = new MaterialDesign();
                //retrieve primary key/id from route data
                materialDesignVm.NewMaterialDesign.MaterialDesignId =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(materialDesignVm.NewMaterialDesign).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
