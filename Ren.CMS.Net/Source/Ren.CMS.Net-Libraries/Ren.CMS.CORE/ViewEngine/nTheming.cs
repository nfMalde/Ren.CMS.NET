namespace Ren.CMS.ViewEngine
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    using Ren.CMS;
    using Ren.CMS.CORE.Settings;

    public class nTheming : RazorViewEngine
    {
        #region Fields

        private static readonly string[] _emptyLocations;

        #endregion Fields

        #region Constructors

        public nTheming()
        {
            
            MasterLocationFormats = new[] {
                "~/Views/Backend/{0}.cshtml",
                "~/Views/Backend/{1}/{0}.cshtml",
                "~/Views/Backend/Shared/{0}.cshtml",
                "~/Views/{areaname}/{0}.cshtml",
                "~/Views/{areaname}/{1}/{0}.cshtml",
                "~/Views/{areaname}/Shared/{0}.cshtml",
                
                "~/Views/{2}/{1}/{0}.cshtml",
                "~/Views/{2}/{1}/{0}.vbhtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.vbhtml"
            };
            ViewLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
            "~/Views/Backend/{0}.cshtml",
                "~/Views/Backend/{1}/{0}.cshtml",
                "~/Views/Backend/Shared/{0}.cshtml",

                    "~/Views/{areaname}/{0}.cshtml",
                "~/Views/{areaname}/{1}/{0}.cshtml",
                "~/Views/{areaname}/Shared/{0}.cshtml",
                "~/Views/{2}/{1}/{0}.vbhtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.vbhtml"
            };
            PartialViewLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
               "~/Views/Backend/{0}.cshtml",
                "~/Views/Backend/{1}/{0}.cshtml",
                "~/Views/Backend/Shared/{0}.cshtml",
                 "~/Views/{areaname}/{0}.cshtml",
                "~/Views/{areaname}/{1}/{0}.cshtml",
                "~/Views/{areaname}/Shared/{0}.cshtml",
                "~/Views/{2}/{1}/{0}.vbhtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.vbhtml"
            };
        }

        #endregion Constructors

        #region Methods

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            string[] strArray;
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("partialViewName must be specified.", "partialViewName");
            }

            var themeName = GetThemeToUse(controllerContext);

            var requiredString = controllerContext.RouteData.GetRequiredString("controller");
            var partialViewPath = GetPath(controllerContext, PartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, themeName, requiredString, "Partial", useCache, out strArray);
            return string.IsNullOrEmpty(partialViewPath) ? new ViewEngineResult(strArray) : new ViewEngineResult(CreatePartialView(controllerContext, partialViewPath), this);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string[] strArray;
            string[] strArray2;

            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("viewName must be specified.", "viewName");
            }

            var themeName = GetThemeToUse(controllerContext);

            var requiredString = controllerContext.RouteData.GetRequiredString("controller");

            var viewPath = GetPath(controllerContext, ViewLocationFormats, "ViewLocationFormats", viewName, themeName, requiredString, "View", useCache, out strArray);
            var masterPath = GetPath(controllerContext, MasterLocationFormats, "MasterLocationFormats", masterName, themeName, requiredString, "Master", useCache, out strArray2);

            if (!string.IsNullOrEmpty(viewPath) && (!string.IsNullOrEmpty(masterPath) || string.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
            }

            return new ViewEngineResult(strArray.Union(strArray2));
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            try
            {
                return File.Exists(controllerContext.HttpContext.Server.MapPath(virtualPath));
            }
            catch (HttpException exception)
            {
                if (exception.GetHttpCode() != 0x194)
                {
                    throw;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static string GetThemeToUse(ControllerContext controllerContext)
        {
            //MembershipUser U = new Ren.CMS.MemberShip.nProvider.CurrentUser().nUser;
            //UserSettings USR = new UserSettings(U);
            //string theme = USR.getSetting("USER_THEME").Value.ToString();

               // if (String.IsNullOrEmpty(theme)) theme = System.Configuration.ConfigurationManager.AppSettings["ren_cmsThemeAtFirstRun"].ToString();
               string theme = "ren.cms";
               if (!HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/en-US/Installer"))
               {
                   GlobalSettings GS = new GlobalSettings();
                   nSetting Setting = GS.getSetting("GENERAL_THEME");
                   if (Setting != null && Setting.Value != null && !String.IsNullOrEmpty(Setting.Value.ToString()))
                       theme = (string)Setting.Value;
               }
            return theme;
        }

        private static bool IsSpecificPath(string name)
        {
            var ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string themeName)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}",
                new object[] { GetType().AssemblyQualifiedName, prefix, name, controllerName, themeName });
        }

        private string GetPath(ControllerContext controllerContext, string[] locations, string locationsPropertyName, string name, string themeName, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            if ((locations == null) || (locations.Length == 0))
            {
                throw new InvalidOperationException("locations must not be null or emtpy.");
            }

            bool flag = IsSpecificPath(name);
            string key = CreateCacheKey(cacheKeyPrefix, name, flag ? string.Empty : controllerName, themeName);
            if (useCache)
            {
                var viewLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);
                if (viewLocation != null)
                {
                    return viewLocation;
                }
            }
            return !flag ? GetPathFromGeneralName(controllerContext, locations, name, controllerName, themeName, key, ref searchedLocations)
                        : GetPathFromSpecificName(controllerContext, name, key, ref searchedLocations);
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, string[] locations, string name, string controllerName, string themeName, string cacheKey, ref string[] searchedLocations)
        {
            var virtualPath = string.Empty;
            searchedLocations = new string[locations.Length];
            string areaName = (string)controllerContext.RouteData.DataTokens["area"];

            for (var i = 0; i < locations.Length; i++)
            {
                var str2 = string.Format(CultureInfo.InvariantCulture, locations[i].Replace("{areaname}", areaName), new object[] { name, controllerName, themeName });

                if (FileExists(controllerContext, str2))
                {
                    searchedLocations = _emptyLocations;
                    virtualPath = str2;
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    return virtualPath;
                }
                searchedLocations[i] = str2;
            }

            //Search for AreaName
             

            return virtualPath;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            var virtualPath = name;
            if (!FileExists(controllerContext, name))
            {
                virtualPath = string.Empty;
                searchedLocations = new[] { name };
            }
            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }

        #endregion Methods
    }
}