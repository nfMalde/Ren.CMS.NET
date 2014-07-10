using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ren.CMS.Filemanagement
{
    public static class PathHelper
    {
        /// <summary>
        /// Returns the Base URL of the CMS as String
        /// </summary>
        public static string BaseUrlAsString { get { return _BaseUrl(); } }
        
        /// <summary>
        /// Return the Base URL of the CMS as URI
        /// </summary>
        public static Uri BaseUrl { get {  return new Uri(_BaseUrl());} }

        /// <summary>
        /// Generates the Base URL
        /// </summary>
        /// <returns>BaseUrl String</returns>
        private static string _BaseUrl()
        {
             UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);

             string baseUrl = string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, Url.Content("~"));

             return baseUrl;
        }

        /// <summary>
        /// Generates the URL for an virtual path 
        /// </summary>
        /// <param name="path">the virtual path,  with or without leading ~/ or leading /</param>
        /// <returns>Url</returns>
        public static Uri GetUrlByVirtualPath(string path)
        {
            string url = BaseUrlAsString;
            if(path.StartsWith("~/"))
            {
                path = path.Substring(2);
            }

            if(path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            if (!url.EndsWith("/"))
                url = String.Concat(url, "/");

            url = String.Concat(url, path);

            while (url.Contains("//"))
                url = url.Replace("//", "/");

            return new Uri(url);
        }
        
        /// <summary>
        /// Returns the Base Path of the CMS
        /// </summary>
        public static string BasePath { get { return HttpContext.Current.Server.MapPath("~/"); } }

        public static Uri GetUrlByLocalPath(string path)
        {

            if ( path.StartsWith(BasePath))
            {
                path = path.Substring(BasePath.Length - 1);

                while(path.Contains('\\'))
                {
                    path = path.Replace('\\', '/');
                }

                while(path.Contains("//"))
                {
                    path = path.Replace("//", "/");
                }

                if (!path.StartsWith("/"))
                    path = String.Concat("/", path);

                path = String.Concat("~", path);

                UrlHelper Helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                return GetUrlByVirtualPath(path);




            }

            return null;
           
        }
    }
}
