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
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            UserViewModel userVm = new UserViewModel();
            using (var db = new UserDBContext())
            {
                userVm.UserList = db.Users.ToList();
                userVm.NewUser = new User();
            }
            return View(userVm);
        }

        //Post
        [HttpPost]
        public IActionResult Index(UserViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                using (var db = new UserDBContext())
                {
                    db.Users.Add(userVm.NewUser);
                    db.SaveChanges();
                    //Redirect to get Index GET method
                    return RedirectToAction("Index");
                }
            }
            //Redirect to get Index GET method
            return RedirectToAction("Index");
        }

        //get
        public IActionResult Edit(Guid id)
        {
            //instantiate view model
            UserViewModel userVm = new UserViewModel();
            using (UserDBContext db = new UserDBContext())
            {
                //assign view model object to value of current object id passed in route
                userVm.NewUser = db.Users.Where(
                    e => e.UserID == id).SingleOrDefault();
                //get  lists

                //return the view model
                return View(userVm);
            }
        }

        //POST this is for edit action
        [HttpPost]
        public IActionResult Edit(UserViewModel obj)
        {
            //check to see if posted model is valid
            if (ModelState.IsValid)
            {
                using (UserDBContext db = new UserDBContext())
                {
                    //instantiate object from view model
                    User e = obj.NewUser;
                    //retrieve primary key/id from route data
                    e.UserID = Guid.Parse(RouteData.Values["id"].ToString());
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
            UserViewModel userVm = new UserViewModel();
            using (UserDBContext db = new UserDBContext())
            {
                //instantiate object from view model
                userVm.NewUser = new User();
                //retrieve primary key/id from route data
                userVm.NewUser.UserID =
                    Guid.Parse(RouteData.Values["id"].ToString());
                //mark the record as modified
                db.Entry(userVm.NewUser).State = EntityState.Deleted;
                //persist changes
                db.SaveChanges();
                TempData["ResultMessage"] = "Object deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
