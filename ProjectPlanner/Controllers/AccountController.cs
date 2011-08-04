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
        // GET: /Account/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        public ActionResult Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
            
                User user = new User(model.Username, model.Password.GetHash(), model.Name, model.Email);

                ctx.Users.Add(user);

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
                }
            }

            return View();
        }
    }
}
