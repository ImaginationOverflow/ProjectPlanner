using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPlanner.Models
{
    public class LogOnModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public bool RemindMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string returnUrl { get; set; }
    }

    public class CreateModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }

    public enum Registration
    {
        Open,
        Closed
    }
}