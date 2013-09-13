namespace Ren.CMS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class BackendController : Controller
    {
        #region Methods

        /**
         *Backend Controller
          *Step1: Load the Main View
          *Step2: Load
         *
         *
         */
        //
        // GET: /Backend/
        public ActionResult OS()
        {
            return View("OS");
        }

        #endregion Methods
    }
}