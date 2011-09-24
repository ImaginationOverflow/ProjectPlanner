using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Idea> Suggestions { get; set; }

        public virtual ICollection<Idea> Approved { get; set; }
    }
}
