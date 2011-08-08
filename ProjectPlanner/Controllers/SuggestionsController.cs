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
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            return View(ctx.Users.Single(u => u.Username == User.Identity.Name).Suggestions);
        }

        //
        // GET: /Suggestions/

        public ActionResult Index(string name)
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            string username = ModelState.IsValid ? name : User.Identity.Name;

            return View(ctx.Users.Single(u => u.Username == username).Suggestions);
        }

        //
        // GET: /Suggestions/Supported

        public ActionResult Supported(string name)
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            string username = ModelState.IsValid ? name : User.Identity.Name;

            return View(ctx.Users.Single(u => u.Username == username).SupportedSuggestions);
        }

        //
        // GET: /Suggestions/Approved

        public ActionResult Approved()
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            return View(ctx.ApprovedIdeas);
        }

        //
        // GET: /Suggestions/All

        public ActionResult All()
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            IEnumerable<Idea> allIdeas = null;

            foreach(ICollection<Idea> allOfUser in ctx.Users.Select(p => p.Suggestions)) 
            {
                if (allIdeas == null)
                    allIdeas = allOfUser;
                else
                {
                    allIdeas = allIdeas.Union(allOfUser);
                }
            }


            return View(allIdeas);
        }

        //
        // POST: /Suggestions/Support

        [HttpPost]
        public ActionResult Support(int ideaID)
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            Idea idea = ctx.Users.Select(u => u.Suggestions).Single(l => l.Where(s => s.IdeaID == ideaID).Count() != 0).Single(s => s.IdeaID == ideaID);

            ctx.Users.Single(u => u.Username == User.Identity.Name).SupportedSuggestions.Add(idea);

            return Json(true);
        }

        //
        // POST: /Suggestions/Add
        
        [HttpPost]
        public ActionResult Add(IdeaModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                User currentUser = ctx.Users.SingleOrDefault(u => u.Username.Equals(User.Identity.Name));
                Idea idea = new Idea(model.Name, model.BriefDescription, model.Description);

                currentUser.Suggestions.Add(idea);

                return Json(idea.IdeaID);
            }

            return View();
        }

        
        //
        // POST: /Suggestions/Remove

        [HttpPost]
        public ActionResult Remove(int ideaID, string successUrl)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                User currentUser = ctx.Users.SingleOrDefault(u => u.Username.Equals(User.Identity.Name));
                Idea idea = currentUser.Suggestions.SingleOrDefault(i => i.IdeaID == ideaID);

                currentUser.Suggestions.Remove(idea);

                return Redirect(successUrl);
            }

            return View();
        }

    }
}
