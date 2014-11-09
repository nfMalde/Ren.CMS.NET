namespace InstallModule.Installer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE;
    using Ren.CMS.CORE.Language;
    using Ren.CMS.CORE.Language.LanguageDefaults;
    using Ren.CMS.CORE.Permissions;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.ThisApplication;
    using System.Data.SqlClient;
    using System.Configuration;
    using System.Web.Configuration;
using InstallModule.Installer.Models;
    using NHibernate.Tool.hbm2ddl;
    using Ren.CMS.Persistence.Base;
    public class InstallerController : Controller
    {
        #region Methods

        //
        // GET: /Installer/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult License()
        {
            return View();
        }

        [HttpPost]
        public ActionResult License(InstallModule.Installer.Models.LicenseModel Model)
        {
            if(Model.IAccept)
            {
                Session.Timeout = int.MaxValue;
                Session.Add("__LICENSE__", true);
                return RedirectToActionPermanent("InstallType");
            }

            return View();
        }

        [HttpGet]
        public ActionResult InstallType()
        { 
           if(Session["__LICENSE__"] != null)
           {
               Session.Timeout = int.MaxValue;
               Session.Add("__LICENSE__", true);
               return View();
           }

           return RedirectToActionPermanent("__LICENSE__");
        }

        [HttpPost]
        public ActionResult InstallType(InstallModule.Installer.Models.InstallType Model)
        {
            if (Session["__LICENSE__"] != null)
            {
                Session.Timeout = int.MaxValue;
                Session.Add("__LICENSE__", true);

                if(Model.Type == Models.InstallTypeEnum.full)
                {
                    Session.Add("__INSTALLTYPE__", "full");

                }
                else
                {
                    Session.Add("__INSTALLTYPE__", "update");
                }


                return RedirectToActionPermanent("DatabaseConnection");
            }

            return RedirectToActionPermanent("License");
        }

        public ActionResult DatabaseConnection()
        {
            if (Session["__LICENSE__"] != null)
            {
                Session.Timeout = int.MaxValue;
                Session.Add("__LICENSE__", true);

                return View();
            }

            return RedirectToActionPermanent("License");
        }

        [HttpPost]
        public ActionResult DatabaseConnection(Models.DatabaseConnection Model)
        {
            if (Session["__LICENSE__"] != null && ModelState.IsValid)
            {
                Session.Timeout = int.MaxValue;
                Session.Add("__LICENSE__", true);
                if (Session["__INSTALLTYPE__"] != null)
                    Session.Add("__INSTALLTYPE__", Session["__INSTALLTYPE__"]);
                else
                    return RedirectToActionPermanent("InstallType");
                try
                {
                    SqlConnection Con = new SqlConnection(Model.ConnectionString);
                    Con.Open();

                    Con.Close();
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("ConnectionString", e.Message);
                    return View(Model);
                }

                //Add ConnectionString
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
                section.ConnectionStrings["nfCMS"].ConnectionString = Model.ConnectionString;
                if (configuration.AppSettings.Settings.AllKeys.Any(e => e == "nfcmsSQLPrefix"))
                {
                    configuration.AppSettings.Settings.Remove("nfcmsSQLPrefix");
                }
                configuration.AppSettings.Settings.Add("nfcmsSQLPrefix", Model.TablePrefix);
                configuration.Save();

                return RedirectToActionPermanent("Install");
              
            }

            return RedirectToActionPermanent("License");
        }

        [HttpGet]
        public ActionResult Install()
        {
            if (Session["__LICENSE__"] != null)
            {
                if (Session["__INSTALLTYPE__"] != null)
                    Session.Add("__INSTALLTYPE__", Session["__INSTALLTYPE__"]);
                else
                    return RedirectToActionPermanent("InstallType");

                //Scan Folder: ~/Install/Language
                string folder = Server.MapPath("~/Install/Languages");
                string[] files = System.IO.Directory.GetFiles(folder, "*.lang.xml");
                Models.InstallModel Model = new Models.InstallModel();
                Model.Languages = new List<Models.Languages>();
                Model.FullInstall = (Session["__INSTALLTYPE__"] == "full");

                foreach(string file in files)
                {
                    //Get Header Only
                    string fileName = Path.GetFileName(file);
                    string fpath = Server.MapPath("~/Install/Languages/"+ fileName);

                    LanguageFileReader Reader = new LanguageFileReader(fpath);
                    Model.Languages.Add(new Models.Languages() { FileName = fileName, LanguageName = Reader.GetHeader().Title });
                }
                return View(Model);

            }

            return RedirectToActionPermanent("License");
        }
        
        [HttpPost]
        public JsonResult GetInstallActions()
        {

            List<object> Actions = new List<object>();

            Actions.Add(new { actionName = "InstallDB", title = "Database Structure" });
            Actions.Add(new { actionName = "InstallLanguage", title = "Localization Files" });

            Actions.Add(new { actionName = "InstallSettings", title = "Default Settings" });
            Actions.Add(new { actionName = "InstallPermissions", title = "Permissions and Permissiongroups" });
            Actions.Add(new { actionName = "InstallFilemanagement", title = "Filemanagement System" });
            Actions.Add(new { actionName = "InstallContent", title = "Contentmanagement System" });


            return Json(new { actions = Actions } );
        }
        #endregion Methods

        #region Install Actions

        [HttpPost]
        public JsonResult InstallDB(InstallModel Model)
        {
            try
            {
                var nconfig = Ren.CMS.Persistence.NHibernateHelper.GetConfiguration();
                
                if(Model.FullInstall)
                {
                    new SchemaExport(nconfig).Execute(true, true, false);
                }
                else
                {
                    new SchemaUpdate(nconfig).Execute(true, true);
                }

                return Json(new { success = true });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }


        [HttpPost]
        public JsonResult InstallLanguage(InstallModel Model)
        {
            try
            {
                Ren.CMS.Persistence.Base.BaseRepository<Ren.CMS.Persistence.Domain.LangCode> Repo = new Ren.CMS.Persistence.Base.BaseRepository<Ren.CMS.Persistence.Domain.LangCode>();

                foreach (var l in Model.Languages)
                {
                    string file = Server.MapPath("~/Install/Languages/" + l.FileName);
                    LanguageFileReader Reader = new LanguageFileReader(file);
                    LanguageFileHeader Header = Reader.GetHeader();
                    var one = Repo.GetOne(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.LangCode>(e => e.Code == Header.LangCode));
                    if (one == null && !String.IsNullOrEmpty(Header.LangName))
                    {
                        Ren.CMS.Persistence.Domain.LangCode NewCode = new Ren.CMS.Persistence.Domain.LangCode();
                        NewCode.Code = Header.LangCode;
                        NewCode.Name = Header.LangName;
                        Repo.Add(NewCode);
                    }

                    foreach (var line in Reader.GetLines())
                    {
                        Language L = new Language(Header.LangCode, line.LanguagePackage);

                        if(Model.FullInstall || !L.LanglineExists(line.LanguageLineName, line.LanguagePackage, Header.LangCode))
                            L.InsertLine(line.LanguageLineName, line.LanguageLineValue, true);
                    }

                }

                return Json(new { success = true });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public JsonResult InstallSettings(InstallModel Model)
        {

            GlobalSettings Settings = new GlobalSettings();
            //Install Global Settings

            //Site Settings
            if(Model.FullInstall ||  Settings.getSetting("SITE_TITLE") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_TITLE";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_TITLE";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;
                MODEL.Value = "Ren.CMS.NET - Free Open Source CMS";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("SITE_DESCIRPTION") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_DESCIRPTION";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_DESCRIPTION";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;
                MODEL.Value = "This is your site descirption for Meta Tags";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("SITE_KEYWORDS") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_KEYWORDS";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_KEYWORDS";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;
                MODEL.Value = "Keywords, for, your, site";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("SITE_OFFLINE") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_OFFLINE";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_OFFLINE";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;
                MODEL.Value = true;
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("SITE_URL_HTTP") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_URL_HTTP";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_URL_HTTP";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("SITE_URL_HTTPS") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_URL_HTTPS";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_URL_HTTPS";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = string.Format("{0}://{1}{2}", "https", Request.Url.Authority, Url.Content("~"));
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("SITE_INSTALLATION_DIR") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "SITE_INSTALLATION_DIR";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_INSTALLATION_DIR";
                MODEL.SettingRelation = "SITE_SETTINGS";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = Server.MapPath("~/");
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            //General
            if (Model.FullInstall || Settings.getSetting("GENERAL_THEME") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "GENERAL_THEME";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_GENERAL_THEME";
                MODEL.SettingRelation = "GENERAL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "ren.cms";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("GENERAL_DEFAULT_LANGUAGE") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "GENERAL_DEFAULT_LANGUAGE";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_GENERAL_DEFAULT_LANGUAGE";
                MODEL.SettingRelation = "GENERAL";
                MODEL.SettingType = nSettingType.SettingString;

                BaseRepository<Ren.CMS.Persistence.Domain.LangCode> Repo = new BaseRepository<Ren.CMS.Persistence.Domain.LangCode>();
                var list = Repo.GetMany();
                if (list.Any(e => e.Code == "en-US"))
                    MODEL.Value = "en-US";
                else
                    MODEL.Value = list.First().Code;

                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }


            if (Model.FullInstall || Settings.getSetting("GENERAL_FORCELANGUAGE") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "GENERAL_FORCELANGUAGE";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_GENERAL_FORCELANGUAGE";
                MODEL.SettingRelation = "GENERAL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = false;
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }
            
            //Mail Settings

            if (Model.FullInstall || Settings.getSetting("MAIL_FROM") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_FROM";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_FROM";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "ren.cms@" + Request.Url.Authority;
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("MAIL_TO") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_TO";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_TO";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "ren.cms@" + Request.Url.Authority;
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }


            if (Model.FullInstall || Settings.getSetting("MAIL_NAME") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_NAME";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_NAME";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "Ren.CMS Admin";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("MAIL_SMTP_SERVER") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_SMTP_SERVER";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_SERVER";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("MAIL_SMTP_PORT") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_SMTP_PORT";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_PORT";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }


            if (Model.FullInstall || Settings.getSetting("MAIL_SMTP_USER") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_SMTP_USER";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_USER";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            if (Model.FullInstall || Settings.getSetting("MAIL_SMTP_PASSWORD") == null)
            {
                nSetting MODEL = new nSetting();
                MODEL.Name = "MAIL_SMTP_PASSWORD";
                MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_PASSWORD";
                MODEL.SettingRelation = "MAIL";
                MODEL.SettingType = nSettingType.SettingString;

                MODEL.Value = "";
                MODEL.ValueType = nValueType.ValueString;

                Settings.AddSetting(MODEL);

            }

            return Json(new {success =true});

        }
        #endregion 
    }
}