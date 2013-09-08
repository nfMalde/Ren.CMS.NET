namespace Ren.CMS.Install
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using MvcContrib;
    using MvcContrib.PortableAreas;

    using Ren.CMS.ViewEngine;

    public class InstallRegistration : PortableAreaRegistration
    {
        #region Properties

        public override string AreaName
        {
            get { return "Installer"; }
        }

        #endregion Properties

        #region Methods

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            /*context.MapRoute(
                "News_Default",
                 AreaName +"{controller}/{action}/{id}/{category}/{subcategory}",
                new { controller = "News", action = "Archive", id=UrlParameter.Optional, category =  UrlParameter.Optional, subcategory=UrlParameter.Optional }

                );
              */

            string[] thisNamespace = { "Ren.CMS.Install" };

            /*
            context.MapRoute(
            "News_Show",
             AreaName + "{controller}/{action}/{id}",
            new { controller = "News", action = "Show", id = "" },
            new string[] { "MvcContrib.PortableAreas" }

            );*/
            RegisterAreaEmbeddedResources();
        }

        #endregion Methods
    }
}