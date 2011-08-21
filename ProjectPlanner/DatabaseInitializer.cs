using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Model;

namespace ProjectPlanner
{
    public class DatabaseInitializer : IDatabaseInitializer<ProjectPlannerContext>
    {
        public void InitializeDatabase(ProjectPlannerContext context)
        {
            context.Database.Connection.ConnectionString = @"Data Source=JOAO-PC\(local);Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";
        }
    }
}