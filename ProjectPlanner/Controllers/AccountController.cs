using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using ProjectPlanner.Models;
using System.Web.Security;
using System.Web.Configuration;
using ProjectPlanner.Utils;
using System.Data.Entity.Infrastructure;

namespace ProjectPlanner.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
            
                User user = new User { Username = model.Username, PasswordHash = model.Password.GetHash(), Name = model.Name, Email = model.Email };

                if (ctx.Users.SingleOrDefault(u => u.Username == model.Username) != null)
                {
                    ViewBag.Error = "Username already exists!";

                    return View(model);
                }

                ctx.Users.Add(user);
                ctx.SaveChanges();
                return LogOn(new LogOnModel { Username = model.Username, Password = model.Password, RemindMe = false }, Url.Action("Index", "Home"));
            }            

            return null;
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            if (ModelState.IsValid)
            {
                string passwordHash = model.Password.GetHash();
                if(ctx.Users.SingleOrDefault(p => (p.Username.Equals(model.Username) && p.PasswordHash.Equals(passwordHash))) != null) 
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RemindMe);

                    return Redirect(returnUrl);
                }

                ViewBag.Error = "Couldn't find the user or password did not match!";
            }

            return View();
        }

        //
        // GET: /Account/LogOut

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
