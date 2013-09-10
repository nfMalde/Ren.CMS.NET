namespace Ren.CMS.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    public static class HeadLoader
    {
        #region Methods

        public static void addCSS(string cssPath, ControllerContext context, string rel = "stylesheet")
        {
            string css = "<link rel=\"" + rel + "\" href=\"" + cssPath + "\" />";
            context.Controller.ViewData["additional____CSS"] += css;
        }

        public static void addScript(string jspath, ControllerContext context,string type="text/javascript")
        {
            string script = "<script type=\"" + type + "\" src=\"" + jspath + "\"></script>";

            context.Controller.ViewData["additional____Scripts"] += script;
        }

        public static IHtmlString LoadHeadDataFromViews(ControllerContext context, bool loadScriptsFirst = true)
        {
            string scripts = (context.Controller.ViewData["additional____Scripts"] != null ? context.Controller.ViewData["additional____Scripts"].ToString() : "");
            string css = (context.Controller.ViewData["additional____CSS"] != null ? context.Controller.ViewData["additional____CSS"].ToString() : "");
            string headAdd = "";

            if (loadScriptsFirst)
            {

                headAdd = scripts;
                headAdd += css;

            }
            else {
                headAdd = css;
                headAdd += scripts;

            }

            return new HtmlString(headAdd);
        }

        #endregion Methods
    }
}