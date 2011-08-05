using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Idea
    {
        public Idea(string name, string briefdescription, string description)
        {
            Name = name;
            BriefDescription = briefdescription;
            Description = description;
        }

        public int IdeaID { get; set; }

        public string Name { get; set; }

        public string BriefDescription { get; set; }

        public string Description { get; set; }
    }
}
