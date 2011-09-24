using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPlanner.Models;
using Model;
using System.Configuration;

namespace ProjectPlanner.Controllers
{
    public class SuggestionsController : Controller
    {
        //
        // GET: /Suggestions/

        public ActionResult Index(string name)
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            string username = name != null ? name : User.Identity.Name;

            return View(ctx.Users.Single(u => u.Username.Equals(username)).Suggestions);
        }

        //
        // GET: /Suggestions/Supported

        public ActionResult Supported(string name)
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            string username = name != null ? name : User.Identity.Name;

            return View(ctx.Ideas.Where(i => i.Approvers.Contains(ctx.Users.FirstOrDefault(u => u.Username.Equals(username)))));
        }

        //
        // GET: /Suggestions/Approved

        public ActionResult Approved()
        {
            ProjectPlannerContext ctx = new ProjectPlannerContext();

            int approvingThreshold = int.Parse(ConfigurationManager.AppSettings["ApprovingThreshold"]);
            return View(ctx.Ideas.Where(i => i.Approvers.Count > approvingThreshold));
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
            User approver = ctx.Users.Single(u => u.Username.Equals(User.Identity.Name));

            if (idea.Approvers == null) idea.Approvers = new List<User>();
            idea.Approvers.Add(approver);
            ctx.SaveChanges();

            return View("Supported");
        }

        // 
        // GET: /Suggestions/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Suggestions/Create
        
        [HttpPost]
        public ActionResult Create(IdeaModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                User currentUser = ctx.Users.SingleOrDefault(u => u.Username.Equals(User.Identity.Name));
                Idea idea = new Idea { Name = model.Name, BriefDescription = model.BriefDescription, Description = model.Description };

                currentUser.Suggestions.Add(idea);

                ctx.SaveChanges();

                return Json(idea.IdeaID);
            }

            return View(model);
        }
        
        //
        // GET: /Suggestions/Details

        public ActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                
                IEnumerable<Idea> allIdeas = null;
                foreach(ICollection<Idea> allOfUser in ctx.Users.Select(p => p.Suggestions)) 
                {
                    if (allIdeas == null)
                        allIdeas = allOfUser;
                    else
                        allIdeas = allIdeas.Union(allOfUser);
                }
                allIdeas = allIdeas.Union(ctx.Ideas);
                
                Idea idea = allIdeas.SingleOrDefault(i => i.IdeaID == id);
                return View(idea);
            }

            throw new InvalidOperationException("IdeaID unexistant or invalid");
        }

        //
        // POST: /Suggestions/Remove

        [HttpPost]
        public ActionResult Remove(int ideaID)
        {
            if (ModelState.IsValid)
            {
                ProjectPlannerContext ctx = new ProjectPlannerContext();
                User currentUser = ctx.Users.SingleOrDefault(u => u.Username.Equals(User.Identity.Name));
                Idea idea = currentUser.Suggestions.SingleOrDefault(i => i.IdeaID == ideaID);

                currentUser.Suggestions.Remove(idea);

                ctx.SaveChanges();

                return View();
            }

            return View();
        }

    }
}
