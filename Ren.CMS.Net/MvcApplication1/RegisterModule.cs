namespace Ren.CMS.Article
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

    public class ArticleRegistration : PortableAreaRegistration
    {
        #region Properties

        public override string AreaName
        {
            get { return "Article"; }
        }

        #endregion Properties

        #region Methods

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            /*context.MapRoute(
                "Article_Default",
                 AreaName +"{controller}/{action}/{id}/{category}/{subcategory}",
                new { controller = "Article", action = "Archive", id=UrlParameter.Optional, category =  UrlParameter.Optional, subcategory=UrlParameter.Optional }

                );
              */

            string[] synonems = new string[] { "Kategorie", "Category" };
            int x = 0;
            foreach (string s in synonems) {

                x++;

            }

            context.MapRoute(
              "Article_Show_Paging",
               "Article/Show/{id}-{title}-{page}",
              new { controller = "Article", action = "Show", id="", page=""}

              );
            context.MapRoute(
            "Article_Show_Paging2",
             "Article/Show/{id}-{title}",
            new { controller = "Article", action = "Show", id = "" }

            );

            context.MapRoute(
               "Article_Tag",
            "Article/Tag/{tgn}-{page}",
               new { controller = "Article", action = "Tag", tgn = "", page = "" }

               );
            context.MapRoute(
              "Article_Tag2",
               "Article/Tag/{tgn}",
              new { controller = "Article", action = "Tag", tgn = ""}

              );

            context.MapRoute(
            "Article_Category",
             "Article/Category/{category}-{page}",
            new { controller = "Article", action = "Category", category = "", page = "" }

            );
            context.MapRoute(
               "Article_Category2",
            "Article/Category/{category}",
               new { controller = "Article", action = "Category", category = "" }

               );

             context.MapRoute(
            "Article_SCategory",
            "Article/SubCategory/{category}-{subname}-{page}",
            new { controller = "Article", action = "SubCategory", category = "", subname="", page = "" }

            );
            context.MapRoute(
               "Article_SCategory2",
            "Article/SubCategory/{category}-{subname}",
               new { controller = "Article", action = "SubCategory", category = "", subname="" }

               );

            /*
            context.MapRoute(
            "Article_Show",
             AreaName + "{controller}/{action}/{id}",
            new { controller = "Article", action = "Show", id = "" },
            new string[] { "MvcContrib.PortableAreas" }

            );*/
            RegisterAreaEmbeddedResources();
        }

        #endregion Methods
    }
}