namespace Ren.CMS.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using MvcContrib;
    using MvcContrib.PortableAreas;

    using Ren.CMS.CORE.Helper.RoutingHelper;
    using Ren.CMS.ViewEngine;

    public class BlogRegistration : PortableAreaRegistration
    {
        #region Properties

        public override string AreaName
        {
            get { return "Blog"; }
        }

        #endregion Properties

        #region Methods

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            /*context.MapRenCMSRoute(
                "Blog_Default",
                 AreaName +"{controller}/{action}/{id}/{category}/{subcategory}",
                new { controller = "Blog", action = "Archive", id=UrlParameter.Optional, category =  UrlParameter.Optional, subcategory=UrlParameter.Optional }

                );
              */

            string[] synonems = new string[] { "Kategorie", "Category" };
            int x = 0;
            foreach (string s in synonems) {

                x++;

            }

            string[] thisNamespace = { "Ren.CMS.Blog" };

            context.MapRenCMSRoute("NewsIndex",
                "Blog/{contentType}/{id}-{title}-{page}",
                  new { controller = "Blog", action = "Show", id = "" }
              , thisNamespace);

            context.MapRenCMSRoute("NewsIndex2",
                "Blog/{contentType}/{id}-{title}",
                  new { controller = "Blog", action = "Show", id = "", page = "" }
              , thisNamespace);

            context.MapRenCMSRoute("Archive",

                "Blog/{contentType}",
                new { controller = "Blog", action = "Archive", id = "", page =1 }
                );

            context.MapRenCMSRoute("Archive2",

                "Blog/{contentType}/{page}",
                new { controller = "Blog", action = "Archive", id = "", page = "" }
                );

            context.MapRenCMSRoute("NewsAddCommentAnswer",
                "Blog/{contentType}/PartialCommentAnswers",
                  new { controller = "Blog", action = "PartialCommentAnswers", NewsID = 0, CommentID = 0, Page = 1 }
              , thisNamespace);

            context.MapRenCMSRoute(
              "Blog_Show_Paging",
               "Blog/{contentType}/Show/{id}-{title}-{page}",
              new { controller = "Blog", action = "Show", id="", page=""}
              , thisNamespace

              );
            context.MapRenCMSRoute(
            "Blog_Show_Paging2",
             "Blog/{contentType}/Show/{id}-{title}",
            new { controller = "Blog", action = "Show", id = "" }
            , thisNamespace

            );

            context.MapRenCMSRoute(
               "Blog_Tag",
            "Blog/{contentType}/Tag/{tgn}-{page}",
               new { controller = "Blog", action = "Tag", tgn = "", page = "" }
               , thisNamespace
               );
            context.MapRenCMSRoute(
              "Blog_Tag2",
               "Blog/{contentType}/Tag/{tgn}",
              new { controller = "Blog", action = "Tag", tgn = ""}
              , thisNamespace
              );

            context.MapRenCMSRoute(
            "Blog_Category",
             "Blog/{contentType}/Category/{category}-{page}",
            new { controller = "Blog", action = "Category", category = "", page = "" }
            , thisNamespace
            );
            context.MapRenCMSRoute(
               "Blog_Category2",
            "Blog/{contentType}/Category/{category}",
               new { controller = "Blog", action = "Category", category = "" }
               , thisNamespace
               );

            /*
            context.MapRenCMSRoute(
            "Blog_Show",
             AreaName + "{controller}/{action}/{id}",
            new { controller = "Blog", action = "Show", id = "" },
            new string[] { "MvcContrib.PortableAreas" }

            );*/
            RegisterAreaEmbeddedResources();
        }

        #endregion Methods
    }
}