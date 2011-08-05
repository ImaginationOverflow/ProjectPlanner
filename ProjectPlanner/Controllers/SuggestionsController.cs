using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPlanner.Models;
using Model;

namespace ProjectPlanner.Controllers
{
    public class SuggestionsController : Controller
    {
        //
        // GET: /Suggestions/

        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Suggestions/Add
        [HttpPost]
        public ActionResult Add(IdeaModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                User currentUser = ctx.Users.SingleOrDefault(u => u.Username.Equals(User.Identity.Name));
                Idea idea = new Idea(model.Name, model.BriefDescription, model.Description);

                currentUser.Suggestions.Add(idea);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Remove(int ideaID)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                User currentUser = ctx.Users.SingleOrDefault(u => u.Username.Equals(User.Identity.Name));
                Idea idea = currentUser.Suggestions.SingleOrDefault(i => i.IdeaID == ideaID);

                currentUser.Suggestions.Remove(idea);
            }

            return View();
        }

    }
}
