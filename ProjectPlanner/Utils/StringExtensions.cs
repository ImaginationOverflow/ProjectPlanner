using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectPlanner.Utils
{
    public static class StringExtensions
    {

        public static string GetHash(this string value)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "SHA1");
        }
        

    }
}