using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Idea
    {
        public int IdeaID { get; set; }

        public string Name { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }

        public int CreatorID { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<User> Approvers { get; set; }
    }
}
