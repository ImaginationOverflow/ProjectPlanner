using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Model
{
    public class ProjectPlannerContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Idea> ApprovedIdeas { get; set; }

    }
}
