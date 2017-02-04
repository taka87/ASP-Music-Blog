using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Blog.Models;

namespace Blog.Classes
{
    public class Utils
    {
        public static string CutText(string text, int maxLength = 100)
        {
            if (text.Length == 0 || text.Length <= maxLength)
                return text;
            var shortText = text.Substring(0, maxLength) + "...";
            return shortText;

        }
    }
}