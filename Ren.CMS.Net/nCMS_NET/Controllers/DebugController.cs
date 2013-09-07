using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.CORE.Settings;
using Ren.CMS.CORE.SqlHelper;
using System.IO;
using Ren.CMS.CORE.Permissions;


namespace Ren.CMS.Controllers
{
    public class UserTest {
        //required model properties
        public string TableName { get; set; }
        public string IdentityName { get; set; }
        public object IdentityValue { get; set; }


        public object PKID { get; set; }
        public string UserName { get; set; }
    
    }
    public class DebugController : Controller
    {
  

    }
}
