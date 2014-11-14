﻿namespace Ren.CMS.CORE.ThisApplication
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    public class ThisApplication
    {
        #region Constructors

        public ThisApplication()
        {
        }

        #endregion Constructors

        #region Properties

        public string ApplicationName
        {
            get { return HttpContext.Current.Request.ApplicationPath.Substring(1, HttpContext.Current.Request.ApplicationPath.Length - 1); }
        }

        public string BaseUrl
        {
            get
            {

                try
                {

                    if (HttpContext.Current.Request.Url.Scheme.ToLower() == "https")

                        return System.Configuration.ConfigurationManager.AppSettings["ren_cmsBaseUrlHTTPS"];
                    else return System.Configuration.ConfigurationManager.AppSettings["ren_cmsBaseUrlHTTP"];

                }
                catch
                {
                    return null;

                }

            }
        }

        public string getSqlPrefix
        {
            get
            {

                string str = "";
                try
                {
                    str = ConfigurationManager.AppSettings["ren_cms_SQLPrefix"];
                }
                catch (StackOverflowException e)
                {

                    throw e;

                }
                return str;
            }
            set
            {

                @ConfigurationManager.AppSettings["ren_cms_SQLPrefix"] = value;

            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Checks a boundle of Configvars for EXISTS. If any of them doesnt exists this function will create them.
        /// </summary>
        /// <param name="config">Config Syntax:  name=value  for example:  myconfigvar=MyValue</param>
        public void checkConfig(string[] config)
        {
            for (int x = 0; x < config.Length; x++)
            {

                string[] v = config[x].Split('=');

                if (v.Length > 2 || v.Length < 2) throw new Exception("Incorrect Syntax for Config var: Corrct Format:  name=value    example: <pre><br> MyConfig=true<br>");

                string valueStr = "";
                for (int z = 1; z < v.Length; z++)
                {
                    valueStr += v[z] + "=";
                }
                if (valueStr.EndsWith("=")) valueStr = valueStr.Remove(valueStr.LastIndexOf("="));
                this.createCFGifNotExists(v[0], valueStr);
            };
        }

        private void createCFGifNotExists(string name, string defaultvalue)
        {
            if (ConfigurationManager.AppSettings[name] == null)
            {

                ConfigurationManager.AppSettings.Add(name, defaultvalue);

            }
        }

        #endregion Methods
    }
}