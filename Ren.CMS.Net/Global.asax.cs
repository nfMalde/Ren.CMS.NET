using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using nfCMS_NET.ViewEngine;
//using RouteDebug;
namespace nfCMS_NET
{
    // Hinweis: Anweisungen zum Aktivieren des klassischen Modus von IIS6 oder IIS7 
    // finden Sie unter "http://go.microsoft.com/?LinkId=9394801".

    public class nfCMS : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
         
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
          
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Backend Handler Default
            routes.MapRoute("Backend_Account_LoggedIn",
                            "BackendHandler/Account/LoggedIn",
                            new { controller = "BackendHandlerAccount", action = "LoggedIn" });

            routes.MapRoute("Backend_Account_Logout",
                         "BackendHandler/Account/Logout",
                         new { controller = "BackendHandlerAccount", action = "Logout" });
            routes.MapRoute("Backend_Account_Login",
                            "BackendHandler/Account/Login",
                            new { controller = "BackendHandlerAccount", action = "Login" });

            routes.MapRoute("Backend_Account_Permission",
                          "BackendHandler/Account/CheckPermission",
                          new { controller = "BackendHandlerAccount", action = "CheckPermission" });


            routes.MapRoute("Backend_Layout_MenuCount",
       "BackendHandler/Layout/MenuCount",
       new { controller = "BackendHandlerLayout", action = "MenuCount" });

            routes.MapRoute("Backend_Layout_Menu",
"BackendHandler/Layout/Menu",
new { controller = "BackendHandlerLayout", action = "Menu" });

            routes.MapRoute("Backend_Layout_LoginForm",
            "BackendHandler/Layout/LoginForm",
            new { controller = "BackendHandlerLayout", action = "LoginForm" });


            routes.MapRoute("Backend_Layout_Desktop",
                    "BackendHandler/Layout/Desktop",
                    new { controller = "BackendHandlerLayout", action = "Desktop" });



            routes.MapRoute("Backend_Layout_TimeUpdate",
                    "BackendHandler/Layout/TimeUpdate",
                    new { controller = "BackendHandlerLayout", action = "TimeUpdate" });
            //FIlemanagement


            routes.MapRoute(
                "FileManagement_1",
                "FileManagement/{type}/{reference}/{name}",

                new { controller = "FileManagement", action = "Index", type = "", reference = "", name = "" }, new string[]{"nfCMS_NET.Controllers"});

            //user Avatar
            routes.MapRoute(
            "USER_AVATAR",
            "UserAvatar/{name}",

            new { controller = "UserAvatar", action = "Index", name = "" }, new string[] { "nfCMS_NET.Controllers" });
              
            routes.MapRoute(
                "Default", // Routenname
                "{controller}/{action}/{id}", // URL mit Parametern
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameterstandardwerte
                , new string[] { "nfCMS_NET.Controllers" }
            );

        }

        protected void Application_Start()
        {
            
          
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new nTheming());
            AreaRegistration.RegisterAllAreas();
           
            RegisterGlobalFilters(GlobalFilters.Filters);
            MvcContrib.UI.InputBuilder.InputBuilder.BootStrap();
            RegisterRoutes(RouteTable.Routes);
           
        }
    }
}