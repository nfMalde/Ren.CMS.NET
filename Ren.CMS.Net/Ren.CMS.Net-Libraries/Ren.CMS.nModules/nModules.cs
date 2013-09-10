namespace Ren.CMS.nModules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class nModules
    {
        #region Fields

        private string module = "undefined";

        #endregion Fields

        #region Constructors

        public nModules()
        {
        }

        public nModules(string iModuleName)
        {
            this.ModuleName = iModuleName;
        }

        #endregion Constructors

        #region Properties

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

        #endregion Properties

        #region Methods

        public string ViewPath(string controller, string view)
        {
            if (this.module == "undefined") throw new Exception("Modulename not set. Please register your module in the controllers");
            string myPath = HttpContext.Current.Server.MapPath("~/_Modules/" + this.module + "/Views/" + controller + "/" + view);
            if (!System.IO.File.Exists(myPath)) throw new Exception("View: \"" + myPath + "\" not found.");

            return "~/_Modules/" + this.module + "/Views/" + controller + "/" + view;
        }

        #endregion Methods
    }
}