using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace Model.EntityFramework
{
    public class IdeaConfiguration : EntityTypeConfiguration<Idea>
    {
        public IdeaConfiguration()
        {
            Property(e => e.Name).IsRequired();
            Property(e => e.BriefDescription).IsRequired();
        }
    }
}
