using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.nModules
{
    public class nModules
    {
        private string module = "undefined";

        public nModules()
        {

        }
        public nModules(string iModuleName)
        {

            this.ModuleName = iModuleName;
        }
        public string ModuleName
        {
            get
            {

                return this.module;

            }
            set
            {

                this.module = (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value) ? value : "undefined");

            }


        }
        public string ViewPath(string controller, string view)
        {

            if (this.module == "undefined") throw new Exception("Modulename not set. Please register your module in the controllers");
            string myPath = HttpContext.Current.Server.MapPath("~/_Modules/" + this.module + "/Views/" + controller + "/" + view);
            if (!System.IO.File.Exists(myPath)) throw new Exception("View: \"" + myPath + "\" not found.");


            return "~/_Modules/" + this.module + "/Views/" + controller + "/" + view;



        }



    }
}