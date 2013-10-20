namespace Ren.CMS.NET_InstallProxy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class ServiceController : ApiController
    {
        #region Methods

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        #endregion Methods
    }
}