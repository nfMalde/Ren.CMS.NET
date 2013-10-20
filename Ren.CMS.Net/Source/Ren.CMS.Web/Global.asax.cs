//using RouteDebug;
namespace Ren.CMS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Ren.CMS.CORE.CustomWebConfig.Accessors;
    using Ren.CMS.CORE.Helper;
    using Ren.CMS.CORE.Helper.RoutingHelper;
    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.ViewEngine;

    // Hinweis: Anweisungen zum Aktivieren des klassischen Modus von IIS6 oder IIS7
    // finden Sie unter "http://go.microsoft.com/?LinkId=9394801".
    public class Global : System.Web.HttpApplication
    {
        #region Methods

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRenCMSRoute("BackendUserHandler",
                     "BackendHandler/Users/{action}",
                     new { controller = "BackendHandlerUsers"});

            routes.MapRenCMSRoute("Backend_Content_GetTagData",
                     "BackendHandler/Content/GetTagData",
                     new { controller = "BackendHandlerContent", action = "GetTagData" });

            routes.MapRenCMSRoute("Backend_Content_EditTag",
                     "BackendHandler/Content/EditTag",
                     new { controller = "BackendHandlerContent", action = "EditTag" });

            routes.MapRenCMSRoute("Backend_Content_DeleteTag",
                     "BackendHandler/Content/DeleteTag",
                     new { controller = "BackendHandlerContent", action = "DeleteTag" });

            routes.MapRenCMSRoute("Backend_Content_CreateTag",
                     "BackendHandler/Content/CreateTag",
                     new { controller = "BackendHandlerContent", action = "CreateTag" });

            routes.MapRenCMSRoute("Backend_Content_GetTags",
                     "BackendHandler/Content/GetTags",
                     new { controller = "BackendHandlerContent", action = "GetTags" });

            routes.MapRenCMSRoute("Backend_Content_EditContent",
                         "BackendHandler/Content/EditContent",
                         new { controller = "BackendHandlerContent", action = "EditContent" });
            routes.MapRenCMSRoute("Backend_Content_DeleteContent",
                        "BackendHandler/Content/DeleteContent",
                        new { controller = "BackendHandlerContent", action = "DeleteContent" });

            routes.MapRenCMSRoute("Backend_Content_MoveCategory",
                       "BackendHandler/Content/MoveCategory",
                       new { controller = "BackendHandlerContent", action = "MoveCategory" });

            routes.MapRenCMSRoute("Backend_Content_CatTree_NEW",
                       "BackendHandler/Content/JSTREE_CATEGORIES",
                       new { controller = "BackendHandlerContent", action = "JSTREE_CATEGORIES" });

            routes.MapRenCMSRoute("Backend_Content_CatTree",
                       "BackendHandler/Content/CatTree",
                       new { controller = "BackendHandlerContent", action = "CatTree" });

            routes.MapRenCMSRoute("Backend_Content_GetContentTypes",
                   "BackendHandler/Content/GetContentTypes",
                   new { controller = "BackendHandlerContent", action = "GetContentTypes" });

            routes.MapRenCMSRoute("Backend_Content_DeleteAttachment",
                          "BackendHandler/Content/DeleteAttachment",
                          new { controller = "BackendHandlerContent", action = "DeleteAttachment", id = UrlParameter.Optional });
            routes.MapRenCMSRoute("Backend_Content_AddAttachment",
                          "BackendHandler/Content/AddAttachment",
                          new { controller = "BackendHandlerContent", action = "AddAttachment", id = UrlParameter.Optional });
            routes.MapRenCMSRoute("Backend_Content_GetAttachmentInfo",
                        "BackendHandler/Content/GetAttachmentInfo",
                        new { controller = "BackendHandlerContent", action = "GetAttachmentInfo", id = UrlParameter.Optional });
            routes.MapRenCMSRoute("Backend_Content_EditAttachment",
                   "BackendHandler/Content/EditAttachment",
                   new { controller = "BackendHandlerContent", action = "EditAttachment" });

            routes.MapRenCMSRoute("Backend_Content_GetAttachments",
                        "BackendHandler/Content/GetAttachments/{id}",
                        new { controller = "BackendHandlerContent", action = "GetAttachments", id = UrlParameter.Optional });
            //AddCategory
            routes.MapRenCMSRoute("Backend_Content_Catlist",
                          "BackendHandler/Content/Catlist",
                          new { controller = "BackendHandlerContent", action = "Catlist" });
            //
            routes.MapRenCMSRoute("Backend_Content_ContentList",
                        "BackendHandler/Content/ContentList/{id}",
                        new { controller = "BackendHandlerContent", action = "ContentList", id = UrlParameter.Optional });
            //

            routes.MapRenCMSRoute("Backend_Content_ValidateSEOTitle",
            "BackendHandler/Content/ValidateSEOTitle",
            new { controller = "BackendHandlerContent", action = "ValidateSEOTitle" });

            routes.MapRenCMSRoute("Backend_Content_AddContent",
              "BackendHandler/Content/AddContent/{id}",
              new { controller = "BackendHandlerContent", action = "AddContent", id = "" });

            routes.MapRenCMSRoute("Backend_Content_RemoveCat",
                "BackendHandler/Content/RemoveCat",
                new { controller = "BackendHandlerContent", action = "RemoveCat" });

            routes.MapRenCMSRoute("Backend_Content_EditCategory",
               "BackendHandler/Content/EditCategory",
               new { controller = "BackendHandlerContent", action = "EditCategory" });

            routes.MapRenCMSRoute("Backend_Content_AddCategory",
                  "BackendHandler/Content/AddCategory",
                  new { controller = "BackendHandlerContent", action = "AddCategory" });

            //Backend Handler Default

            routes.MapRenCMSRoute("Backend_Account_GetBGFiles",
                     "BackendHandler/Account/GetBGFiles",
                     new { controller = "BackendHandlerAccount", action = "GetBGFiles" });

            routes.MapRenCMSRoute("Backend_Account_SaveDesktopImage",
                     "BackendHandler/Account/SaveDesktopImage",
                     new { controller = "BackendHandlerAccount", action = "SaveDesktopImage" });

            routes.MapRenCMSRoute("Backend_Account_SaveDekstop",
                            "BackendHandler/Account/SaveDesktop",
                            new { controller = "BackendHandlerAccount", action = "SaveDesktop" });

            routes.MapRenCMSRoute("Backend_Account_LoggedIn",
                            "BackendHandler/Account/LoggedIn",
                            new { controller = "BackendHandlerAccount", action = "LoggedIn" });

            routes.MapRenCMSRoute("Backend_Account_Logout",
                         "BackendHandler/Account/Logout",
                         new { controller = "BackendHandlerAccount", action = "Logout" });
            routes.MapRenCMSRoute("Backend_Account_Login",
                            "BackendHandler/Account/Login",
                            new { controller = "BackendHandlerAccount", action = "Login" });

            routes.MapRenCMSRoute("Backend_Account_Permission",
                          "BackendHandler/Account/CheckPermission",
                          new { controller = "BackendHandlerAccount", action = "CheckPermission" });

            routes.MapRenCMSRoute("Backend_Layout_Widget",
            "BackendHandler/Layout/Widget",
            new { controller = "BackendHandlerLayout", action = "Widget" });

            //AddIcon
            routes.MapRenCMSRoute("Backend_Account_AddIcon",
            "BackendHandler/Account/AddIcon",
            new { controller = "BackendHandlerAccount", action = "AddIcon" });

            routes.MapRenCMSRoute("Backend_Account_RemoveIcon",
            "BackendHandler/Account/RemoveIcon",
            new { controller = "BackendHandlerAccount", action = "RemoveIcon" });

            routes.MapRenCMSRoute("Backend_Account_UpdateIconPos",
               "BackendHandler/Account/UpdateIconPos",
               new { controller = "BackendHandlerAccount", action = "UpdateIconPos" });
            routes.MapRenCMSRoute("Backend_Layout_Icon",
               "BackendHandler/Layout/Icons",
               new { controller = "BackendHandlerLayout", action = "Icons" });
            routes.MapRenCMSRoute("Backend_Layout_RenderIcon",
             "BackendHandler/Layout/RenderIcon",
             new { controller = "BackendHandlerLayout", action = "RenderIcon" });
            routes.MapRenCMSRoute("Backend_Layout_MenuCount",
               "BackendHandler/Layout/MenuCount",
               new { controller = "BackendHandlerLayout", action = "MenuCount" });

            routes.MapRenCMSRoute("Backend_Layout_Menu",
            "BackendHandler/Layout/Menu",
            new { controller = "BackendHandlerLayout", action = "Menu" });

            routes.MapRenCMSRoute("Backend_Layout_LoginForm",
            "BackendHandler/Layout/LoginForm",
            new { controller = "BackendHandlerLayout", action = "LoginForm" });

            routes.MapRenCMSRoute("Backend_Layout_Desktop",
                    "BackendHandler/Layout/Desktop",
                    new { controller = "BackendHandlerLayout", action = "Desktop" });

            routes.MapRenCMSRoute("Backend_Layout_TimeUpdate",
                    "BackendHandler/Layout/TimeUpdate",
                    new { controller = "BackendHandlerLayout", action = "TimeUpdate" });
            //FIlemanagement

            routes.MapRenCMSRoute(
                "FileManagement_1",
                "FileManagement/{type}/{reference}/{name}",

                new { controller = "FileManagement", action = "Index", type = "", reference = "", name = "" }, new string[] { "Ren.CMS.Controllers" });

            //user Avatar
            routes.MapRenCMSRoute(
            "USER_AVATAR",
            "UserAvatar/{name}",

            new { controller = "UserAvatar", action = "Index", name = "" }, new string[] { "Ren.CMS.Controllers" });
            routes.MapRenCMSRoute(
                "Debug", // Routenname
                "Debug/UpdateDB/{id}", // URL mit Parametern
                new { controller = "Debug", action = "UpdateDB", id = UrlParameter.Optional } // Parameterstandardwerte
                , new string[] { "Ren.CMS.Controllers" }
            );
            routes.MapRenCMSRoute(
                "Default", // Routenname
                "{controller}/{action}/{id}", // URL mit Parametern
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameterstandardwerte
                , new string[] { "Ren.CMS.Controllers" }
            );
        }

        protected void Application_BeginRequest(object sender, EventArgs ex)
        {
            /*string type = Request.RequestType;
            string[] ignore = { "post", "httppost" };

            if (!ignore.Any(e => e == type.ToLower()) && !PathIsIgnored())
            {

                    //  throw new Exception(GetPreRoutData());

                    if (GetPreRoutData() == null)
                    {
                        string ISO = writeLanguageForUser();

                        BaseRepository<LangCode> Repo = new BaseRepository<LangCode>();
                        if (Repo.GetMany(NHibernate.Criterion.Expression.Where<LangCode>(e => e.Code == ISO)).Count() == 0)
                        {
                            ISO = writeLanguageForUser(); ;

                        }

                        Response.Redirect(GetFullUrl(ISO));

                    }
                    else
                    {
                       string  ISO = GetPreRoutData();
                       BaseRepository<LangCode> Repo = new BaseRepository<LangCode>();
                       if (Repo.GetMany(NHibernate.Criterion.Expression.Where<LangCode>(e => e.Code == ISO)).Count() == 0)
                       {
                           Response.Redirect(GetFullUrl(writeLanguageForUser()));
                       }

                    }

            }
             */
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

        private string GetFullUrl(string ISO)
        {
            string currentURL = Request.Url.AbsolutePath;

            string url = currentURL.Trim();

            string fullUrl = "/" + ISO + url + (Request.QueryString.Count > 0 ? "?" + Request.QueryString.ToString() : String.Empty);

            return fullUrl;
        }

        private string GetPreRoutData()
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);

            if (!routeData.Values.Any(r => r.Key == "renCMSLanguage"))
                return null;

            return routeData.Values.Where(r => r.Key == "renCMSLanguage").First().Value.ToString();
        }

        private bool PathIsIgnored()
        {
            string currentURL = Request.Url.AbsolutePath;

            string url = currentURL.Trim();

            bool r = RedirectIgnoreConfig.GetConfig().CheckUrlIsIgnored(url);

            return r;
        }

        private string writeLanguageForUser()
        {
            string ISO = String.Empty;
            //We got no Language!
            //Step 1: Try to find Language by Browser Settings
            string[] UserLangs = Request.UserLanguages;

            BaseRepository<LangCode> Repo = new BaseRepository<LangCode>();
            string code = (Request.UserLanguages ?? Enumerable.Empty<string>()).FirstOrDefault();
            if (code.ToLower().StartsWith("de-") || code.ToLower() == "de")
                code = "de-DE";
            var c = Repo.GetOne(NHibernate.Criterion.Expression.Where<LangCode>(e => e.Code == code));
            if (c != null)
            {

                ISO = c.Code;

            }

            if (ISO == String.Empty)
            {
                var langcodes = Repo.GetMany();

                    string[] codex = code.Split('-');

                    if (codex.Count() > 0)
                    {

                        string prelang = codex.First();

                        foreach (var c2 in langcodes)
                        {

                            if (c2.Code.StartsWith(prelang))
                            {
                                ISO = c2.Code;

                                break;

                            }

                        }

                    }
                }

            if (ISO == String.Empty)
                ISO = CurrentLanguageHelper.DefaultLanguage;

            return ISO;
        }

        #endregion Methods
    }
}