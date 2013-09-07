using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ren.CMS.Controllers
{
    public class BackendController : Controller
    {
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

    }
}
