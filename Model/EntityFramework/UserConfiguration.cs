using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace Model.EntityFramework
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).IsRequired();
            Property(u => u.PasswordHash).IsRequired();

            HasKey<string>(u => u.Name);
        }
    }
}
