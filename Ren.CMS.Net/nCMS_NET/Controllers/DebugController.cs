namespace Ren.CMS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.Permissions;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.SqlHelper;

    public class DebugController : Controller
    {
    }

    public class UserTest
    {
        #region Properties

        public string IdentityName
        {
            get; set;
        }

        public object IdentityValue
        {
            get; set;
        }

        public object PKID
        {
            get; set;
        }

        //required model properties
        public string TableName
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        #endregion Properties
    }
}