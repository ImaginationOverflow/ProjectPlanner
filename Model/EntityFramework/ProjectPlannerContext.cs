using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Model
{
    public class ProjectPlannerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdeaConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
