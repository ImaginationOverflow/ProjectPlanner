using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model.EntityFramework;

namespace Model
{
    public class ProjectPlannerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Idea> ApprovedIdeas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdeaConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
