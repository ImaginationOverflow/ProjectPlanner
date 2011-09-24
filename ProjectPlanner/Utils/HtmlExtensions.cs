using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPlanner.Utils
{
    public static class HtmlExtensions
    {

        public static MvcHtmlString Submit(this HtmlHelper helper)
        {
            return helper.Submit("Submit");
        }


        public static MvcHtmlString Submit(this HtmlHelper helper, string value)
        {
            return new MvcHtmlString("<input type=\"submit\" value=\"" + value + "\"></input>");
        }
    }
}