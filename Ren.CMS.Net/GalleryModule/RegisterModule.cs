using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ren.CMS.ViewEngine;
using MvcContrib.PortableAreas;
using MvcContrib;
namespace Ren.CMS.Gallery
{
    public class GalleryRegistration:PortableAreaRegistration
    {   
        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            /*context.MapRoute(
                "News_Default",
                 AreaName +"{controller}/{action}/{id}/{category}/{subcategory}",
                new { controller = "News", action = "Archive", id=UrlParameter.Optional, category =  UrlParameter.Optional, subcategory=UrlParameter.Optional }

                );
          */

            string[] synonems = new string[] { "Kategorie", "Category" };
            int x = 0;
            foreach (string s in synonems) {
                
              

                x++;
            
            
            }

            string[] thisNamespace = { "GalleryModule.Gallery.Controllers" };

            context.MapRoute("Gallery_3",
            "Gallery/GetGalleryNavigation",
            new { controller = "Gallery", action = "GetGalleryNavigation" },
            thisNamespace);
            context.MapRoute("Gallery_1",
                                "Gallery/{reference}/{id}/{type}/{page}",
                                new { controller = "Gallery", reference = "default", action = "Gallery", id = 0, type = "image", page = 1 },
                                thisNamespace);
            context.MapRoute("Gallery_2",
                        "Gallery/{reference}/{id}/{type}",
                        new { controller = "Gallery", reference = "default", action = "Gallery", id = 0, type = "image", page = 1 },
                        thisNamespace);

        
            /*
            context.MapRoute(
            "News_Show",
             AreaName + "{controller}/{action}/{id}",
            new { controller = "News", action = "Show", id = "" },
            new string[] { "MvcContrib.PortableAreas" }

            );*/
            RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get { return "Gallery"; }
        }
    }
}