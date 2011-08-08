using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class User
    {
        public User(string username, string passwordhash, string name, string email)
        {
            Username = username;
            PasswordHash = passwordhash;
            Name = name;
            Email = email;
        }

        public string Name { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Idea> Suggestions { get; set; }

        public virtual ICollection<Idea> SupportedSuggestions { get; set; }
    }
}
