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
            HasKey(u => u.Id);

            Property(u => u.Username).IsRequired();
            Property(u => u.PasswordHash).IsRequired();
            Property(u => u.Name).IsRequired();
        }
    }
}
