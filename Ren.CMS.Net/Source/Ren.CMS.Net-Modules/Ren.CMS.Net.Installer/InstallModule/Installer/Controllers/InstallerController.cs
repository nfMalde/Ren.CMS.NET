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
    using Ren.CMS.Content;
    using InstallModule.Installer.Models;
    using NHibernate.Tool.hbm2ddl;
    using Ren.CMS.Persistence.Base;
    using System.Data.Sql;
    using System.Runtime.InteropServices;
    using System.Data;
    public class InstallerController : Controller
    {
        #region Methods

        //
        // GET: /Installer/
        [HttpGet]
        public ActionResult Index()
        {
            InstallWizardModel Model = new InstallWizardModel();

            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;

            System.Data.DataTable table = instance.GetDataSources();
            Dictionary<string, string> Servers = new Dictionary<string, string>();
            foreach(System.Data.DataRow row in table.Rows)
            {
                string ServerKey = "";
                string ServerDisplay = "";

                if (row["ServerName"] != null)
                {
                    ServerKey = (string)row["ServerName"];
                    ServerDisplay = (string)row["ServerName"];

                }

                if (row["InstanceName"] != null)
                {
                    ServerKey = (string)row["InstanceName"];
                    ServerDisplay = (string)row["InstanceName"];

                }

                //Version
                if (row["Version"] != null)
                {

                    ServerDisplay += " (" + (string)row["Version"] + ")";

                }

                Servers.Add(ServerKey, ServerDisplay);
            }
            SelectList List = new SelectList(Servers,"Key", "Value");
            ViewBag.Servers = List;

            Dictionary<int, string> Auths = new Dictionary<int,string>();
            Auths.Add(1, "Windows Authentification");
            Auths.Add(2, "User Authentification");

            SelectList AuthsList = new SelectList(Auths, "Key", "Value");
            ViewBag.Auths = AuthsList;

            Dictionary<string, LanguageFileReader> LanguageFiles = new Dictionary<string, LanguageFileReader>();
            List<string> ParserErrors = new List<string>();
            //Check for installed versions...
            List<CurrentlyInstalledLanguages> Currently = new List<CurrentlyInstalledLanguages>();
            try
            {
                BaseRepository<Ren.CMS.Persistence.Domain.LangCode> Langs = new BaseRepository<Ren.CMS.Persistence.Domain.LangCode>();
                var listCurrent = Langs.GetMany();
                Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper GS = new Ren.CMS.CORE.SettingsHelper.GlobalSettingsHelper();

                string l = GS.Read("GLOBAL_DEFAULT_LANGUAGE");

                
                foreach(var item in listCurrent)
                {
                    if(l == item.Code)
                    {
                        Model.DefaultLanguageFile = item.Code + ".pre-installed";
                    }

                    BaseRepository<Ren.CMS.Persistence.Domain.tbLanguage> Ls = new BaseRepository<Ren.CMS.Persistence.Domain.tbLanguage>();
                    var cs = Ls.GetMany(NHibernate.Criterion.Expression.Where<Ren.CMS.Persistence.Domain.tbLanguage>(e => e.Code == item.Code));


                    Currently.Add(new CurrentlyInstalledLanguages() { Name = item.Name, LangCode = item.Code, Count = cs.Count(), File = item.Code + ".pre-installed" });

                    
                }

              

            }
            catch { }

            ViewBag.Currently = Currently;

            foreach(string file in System.IO.Directory.GetFiles(Server.MapPath("~/Install/Languages/"),"*.lng.xml"))
            {
                try
                {
                    LanguageFileReader Reader = new LanguageFileReader(Server.MapPath("~/Install/Languages/" + Path.GetFileName(file)));
                    LanguageFiles.Add(Path.GetFileName(file), Reader);
                }
                catch (Exception e)
                {
                    ParserErrors.Add(string.Format("Error in parsing Language file {0}, File is not valid.", file));
                }

            }
            ViewBag.Languages = LanguageFiles;
            ViewBag.LanguageParseErrors = ParserErrors;
            return View(Model);
        }

 

        [HttpPost]
        public JsonResult getConnectionString(GenerateConnectioStringModel Model)
        {
            if(ModelState.IsValid)
            {
                 
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
                builder.DataSource = Model.ServerInstance;
                builder.InitialCatalog = Model.Database;
                
                if(Model.Auth == DBAuthTypeEnum.windows)
                {
                    builder.IntegratedSecurity = true;
                }
                else
                {
                    builder.IntegratedSecurity = false;
                    builder.UserID = Model.ServerUserName;
                    builder.Password = Model.ServerPassword;
                }

                return Json(new { success = true, connectionString = builder.ConnectionString });

            }
            else
            {
                string Message = "Error: Required Fields not filled out.";

                return Json(new { success = false, error = Message });
            }
            //Build Connection String

        }

        [HttpPost]
        public JsonResult testConnection(TestConnectionModel Model)
        {
            bool ok = false;
            string Message = String.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection Con = new SqlConnection(Model.connectionString);
                    Con.Open();

                    Con.Close();

                    ok = true;
                    
                }
                catch(SqlException e)
                {
                    Message = e.Message;
                    ok = false;
                }
                catch (Exception e)
                {
                    Message = e.Message;
                    ok = false;
                }
            }
            else
            {
                Message = "Connection String cannot be empty";
            }
                
                //Connect
            return Json(new { ok = ok, error = Message });
        }
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
        out ulong lpFreeBytesAvailable,
        out ulong lpTotalNumberOfBytes,
        out ulong lpTotalNumberOfFreeBytes);

        [HttpPost]
        public JsonResult SystemRequirements(TestConnectionModel Model)
        {
            //Check
            List<object> Requirements = new List<object>();
            
            ulong RequiredMemoryNonProduction = 512;
            ulong RequiredMemoryProduction = 1024;
            int SQL_VERSION = 10;
            ulong HDDNonProduction = 320;
            ulong HDDProduction = (10)*1024;
        
            bool CAN_WRITE_IN_APP_FOLDER = true;
            bool CAN_WRITE_IN_BIN_FOLDER = true;
            bool CAN_WRITE_IN_VIEW_FOLDER = true;
            ulong lpFreeBytesAvailable = 0;
            ulong lpTotalNumberOfBytes = 0;
            ulong lpTotalNumberOfFreeBytes = 0;
            GetDiskFreeSpaceEx(HttpContext.Server.MapPath("~/"),out lpFreeBytesAvailable, out lpTotalNumberOfBytes, out lpTotalNumberOfFreeBytes);

            
            Microsoft.VisualBasic.Devices.ComputerInfo Info = new Microsoft.VisualBasic.Devices.ComputerInfo();
            Requirements.Add(RequirementObject.GetResult<ulong>(RequiredMemoryNonProduction, Info.AvailableVirtualMemory / 1024 / 1024, "Available Memory for Non-Production", RequirementRule.gt, IfRequirementRuleFails.error));
            Requirements.Add(RequirementObject.GetResult<ulong>(RequiredMemoryProduction, Info.AvailableVirtualMemory / 1024 / 1024, "Available Memory for Production", RequirementRule.gt, IfRequirementRuleFails.warning));
            Requirements.Add(RequirementObject.GetResult<ulong>(HDDNonProduction, lpFreeBytesAvailable / 1024 / 1024, "Available Disc-Space for Non-Production", RequirementRule.gt, IfRequirementRuleFails.error));
            Requirements.Add(RequirementObject.GetResult<ulong>(HDDProduction, lpFreeBytesAvailable / 1024 / 1024, "Available Disc-Space for Production", RequirementRule.gt, IfRequirementRuleFails.warning));

            string actualSQLServer = "Unable to detect SQL Server Version";

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection Con = new SqlConnection(Model.connectionString);
                    Con.Open();

                    string serverVersion = Con.ServerVersion;
                    string[] serverVersionDetails = serverVersion.Split(new string[] { "." }, StringSplitOptions.None);
                    int versionNumber = int.Parse(serverVersionDetails[0]);

                    Requirements.Add(RequirementObject.GetResult<int>(SQL_VERSION, versionNumber, "MS SQL Server Version", RequirementRule.gt, IfRequirementRuleFails.warning));
                     

                }
                catch(Exception e)
                {
                    Requirements.Add(RequirementObject.GetResult<int>(SQL_VERSION, -1, "MS SQL Server Version", RequirementRule.gt, IfRequirementRuleFails.warning));
                     
                }
            }
            
            

            /**
             * 
             *{
             *  cssClass,
             *  title,
             *  required,
             *  actual
             * 
             * } 
             * 
             */

            return Json(new { data = Requirements });
        }

        [HttpPost]
        public JsonResult GetDatabases(GetDatabasesModel Model)
        {
            if(ModelState.IsValid)
            {
                List<object> DBS = new List<object>();
                SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder();
                Builder.DataSource = Model.ServerInstance;
                Builder.IntegratedSecurity = (Model.Auth == DBAuthTypeEnum.windows);
                if(Model.Auth == DBAuthTypeEnum.user)
                {
                    Builder.UserID = Model.ServerUserName;
                    Builder.Password = Model.ServerPassword;

                }

                string constr = Builder.ConnectionString;
                try
                {
                    SqlConnection Con = new SqlConnection(constr);
                    Con.Open();
                    DataTable tblDatabases = Con.GetSchema("Databases");
                    Con.Close();
                    //add to list
                    foreach (DataRow row in tblDatabases.Rows) {
                          String strDatabaseName = row["database_name"].ToString();

                          DBS.Add(new { name = strDatabaseName });


                    }
     
                }
                catch(Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }

                return Json(new { success = true, dbs = DBS });
            }

            return Json(new { success = false, message = "Please check Database connection settings, some requried fields are missing." });
        }
        [HttpPost]
        public ActionResult Index(int id = 0)
        {
            return RedirectToActionPermanent("License");
        }
        [HttpGet]
        public ActionResult License()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult GetSummaryTree()
        {

            return PartialView("_SummaryContent");
        }

        [HttpPost]
        public ActionResult License(InstallModule.Installer.Models.LicenseModel Model)
        {
            if (!ModelState.IsValid)
                return RedirectToActionPermanent("Index");

            if(Model.IAccept)
            {
                Session.Timeout = 60;
                Session.Add("__LICENSE__", true);
                return RedirectToActionPermanent("InstallType");
            }
            else
            {
                ModelState.AddModelError("IAccept", new Exception("You didn´t accept the license."));
            }

            return View(Model);
        }

        [HttpGet]
        public ActionResult InstallType()
        { 
           if(Session["__LICENSE__"] != null)
           {
               Session.Timeout = 60;
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
                Session.Timeout = 60;
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
                Session.Timeout = 60;
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
                Session.Timeout = 60;
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
                section.ConnectionStrings["ren_cms"].ConnectionString = Model.ConnectionString;
                if (configuration.AppSettings.Settings.AllKeys.Any(e => e == "ren_cms_SQLPrefix"))
                {
                    configuration.AppSettings.Settings.Remove("ren_cms_SQLPrefix");
                }
                configuration.AppSettings.Settings.Add("ren_cms_SQLPrefix", Model.TablePrefix);
                configuration.Save();

                return RedirectToActionPermanent("Install");
              
            }

            return RedirectToActionPermanent("License");
        }
 
        [HttpPost]
        public JsonResult WebConfig(InstallWizardModel Model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection Con = new SqlConnection(Model.connectionString);
                    Con.Open();

                    Con.Close();
                }
                catch (Exception e)
                {
                    
                    return Json(new { success = false, message = e.Message });
                }

                //Add ConnectionString
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
                section.ConnectionStrings["ren_cms"].ConnectionString = Model.connectionString;
                if (configuration.AppSettings.Settings.AllKeys.Any(e => e == "ren_cms_SQLPrefix"))
                {
                    configuration.AppSettings.Settings.Remove("ren_cms_SQLPrefix");
                }
                configuration.AppSettings.Settings.Add("ren_cms_SQLPrefix", Model.Prefix);
                configuration.Save();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = ModelState.ToList() });
            }

        }

        [HttpPost]
        public JsonResult GetInstallActions()
        {

            List<object> Actions = new List<object>();

            Actions.Add(new { url = Url.Action("WebConfig"), actionName = "WebConfig", title = "Web Configuration" });
            
            Actions.Add(new { url = Url.Action("InstallDB"), actionName = "InstallDB", title = "Database Structure" });
            Actions.Add(new { url = Url.Action("InstallLanguage"), actionName = "InstallLanguage", title = "Localization Files" });

            Actions.Add(new { url = Url.Action("InstallSettings"), actionName = "InstallSettings", title = "Default Settings" });
            Actions.Add(new { url = Url.Action("InstallPermissions"), actionName = "InstallPermissions", title = "Permissions and Permissiongroups" });
            Actions.Add(new { url = Url.Action("InstallFilemanagement"), actionName = "InstallFilemanagement", title = "Filemanagement System" });
            Actions.Add(new { url = Url.Action("InstallContent"), actionName = "InstallContent", title = "Contentmanagement System" });
            Actions.Add(new { url = Url.Action("InstallWidgets"), actionName = "InstallWidgets", title = "Backend Applications" });
            Actions.Add(new { url = Url.Action("InstallDesktop"), actionName = "InstallDesktop", title = "Backend Desktops" });



            return Json(new { success = true,  actions = Actions } );
        }
        #endregion Methods

        #region Install Actions

        [HttpPost]
        public JsonResult InstallDB(InstallWizardModel Model)
        {
            try
            {
                var nconfig = Ren.CMS.Persistence.NHibernateHelper.GetConfiguration();
                
                if(Model.InstallationType == InstallTypeEnum.full)
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
        public JsonResult InstallLanguage(InstallWizardModel Model)
        {
            try
            {
                Ren.CMS.Persistence.Base.BaseRepository<Ren.CMS.Persistence.Domain.LangCode> Repo = new Ren.CMS.Persistence.Base.BaseRepository<Ren.CMS.Persistence.Domain.LangCode>();

                foreach (var l in Model.Languages)
                {
                    if (!l.EndsWith(".lng.xml"))
                        continue;
                    string file = Server.MapPath("~/Install/Languages/" + l);
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

                        if(Model.InstallationType == InstallTypeEnum.full || !L.LanglineExists(line.LanguageLineName, line.LanguagePackage, Header.LangCode))
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

        public JsonResult InstallSettings(InstallWizardModel Model)
        {
            try
            {
                GlobalSettings Settings = new GlobalSettings();
                //Install Global Settings

                //Site Settings
                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_TITLE") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_TITLE";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_TITLE";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;
                    MODEL.Value = "Ren.CMS.NET - Free Open Source CMS";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_DESCIRPTION") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_DESCIRPTION";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_DESCRIPTION";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;
                    MODEL.Value = "This is your site descirption for Meta Tags";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_KEYWORDS") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_KEYWORDS";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_KEYWORDS";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;
                    MODEL.Value = "Keywords, for, your, site";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_OFFLINE") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_OFFLINE";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_OFFLINE";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;
                    MODEL.Value = true;
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_URL_HTTP") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_URL_HTTP";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_URL_HTTP";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full  || Settings.getSetting("GLOBAL_DEFAULT_LANGUAGE") != null)
                {
                    var Setting = Settings.getSetting("GLOBAL_DEFAULT_LANGUAGE");
                    if (Model.DefaultLanguageFile.EndsWith(".pre-installed"))
                    {
                        Setting.Value = Model.DefaultLanguageFile.Remove(Model.DefaultLanguageFile.IndexOf('.'));
                    }
                    else
                        Setting.Value = new LanguageFileReader(Server.MapPath("~/Install/Languages/" + Model.DefaultLanguageFile)).GetHeader().LangCode;
                    Setting.DefaultValue = Setting.Value;
                    Settings.setValue(Setting);
                }
                else
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "GLOBAL_DEFAULT_LANGUAGE";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_DEFAULT_LANGUAGE";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = new LanguageFileReader(Server.MapPath("~/Install/Languages/" + Model.DefaultLanguageFile)).GetHeader().LangCode;
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);
                }



                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_URL_HTTPS") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_URL_HTTPS";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_URL_HTTPS";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = string.Format("{0}://{1}{2}", "https", Request.Url.Authority, Url.Content("~"));
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("SITE_INSTALLATION_DIR") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "SITE_INSTALLATION_DIR";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_SITE_INSTALLATION_DIR";
                    MODEL.SettingRelation = "SITE_SETTINGS";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = Server.MapPath("~/");
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                //General
                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("GENERAL_THEME") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "GENERAL_THEME";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_GENERAL_THEME";
                    MODEL.SettingRelation = "GENERAL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "ren.cms";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }
                /*
                            if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("GENERAL_DEFAULT_LANGUAGE") == null)
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
                                MODEL.DefaultValue = MODEL.Value;
                                Settings.AddSetting(MODEL);

                            }
                            */

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("GENERAL_FORCELANGUAGE") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "GENERAL_FORCELANGUAGE";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_GENERAL_FORCELANGUAGE";
                    MODEL.SettingRelation = "GENERAL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = false;
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                //Mail Settings

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_FROM") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_FROM";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_FROM";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "ren.cms@" + Request.Url.Authority;
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_TO") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_TO";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_TO";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "ren.cms@" + Request.Url.Authority;
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }


                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_NAME") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_NAME";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_NAME";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "Ren.CMS Admin";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_SMTP_SERVER") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_SMTP_SERVER";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_SERVER";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_SMTP_PORT") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_SMTP_PORT";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_PORT";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }


                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_SMTP_USER") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_SMTP_USER";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_USER";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                if (Model.InstallationType == InstallTypeEnum.full || Settings.getSetting("MAIL_SMTP_PASSWORD") == null)
                {
                    nSetting MODEL = new nSetting();
                    MODEL.Name = "MAIL_SMTP_PASSWORD";
                    MODEL.LabelLanguageLine = "LANG_GLOBALSETTINGS_MAIL_SMTP_PASSWORD";
                    MODEL.SettingRelation = "MAIL";
                    MODEL.SettingType = nSettingType.SettingString;

                    MODEL.Value = "";
                    MODEL.ValueType = nValueType.ValueString;
                    MODEL.DefaultValue = MODEL.Value;
                    Settings.AddSetting(MODEL);

                }

                return Json(new { success = true });
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }

        }


        public JsonResult InstallPermissions(InstallWizardModel Model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "Required fields are empty" });

            Dictionary<string, bool> PermissionKeys = new Dictionary<string, bool>();
            PermissionKeys.Add("USR_CAN_ENTER_BACKEND", false);
            PermissionKeys.Add("USR_CAN_DELETE_CONTENTS", false);
            PermissionKeys.Add("USR_CAN_EDIT_CONTENT", false);
            PermissionKeys.Add("USR_CAN_CREATE_CONTENT", false);
            PermissionKeys.Add("USR_CAN_UPLOAD_CONTENT_ATTACHMENTS", false);
            PermissionKeys.Add("USR_CAN_ADD_CONTENT_CATEGORY", false);
            PermissionKeys.Add("USR_CAN_ADD_CONTENT_TAGS", false);
            PermissionKeys.Add("USR_CAN_DELETE_CONTENT_ATTACHMENTS", false);
            PermissionKeys.Add("USR_CAN_DELETE_CONTENT_TAGS", false);
            PermissionKeys.Add("USR_CAN_EDIT_CONTENT_ATTACHMENTS", false);
            PermissionKeys.Add("USR_CAN_EDIT_CONTENT_TAGS", false);
            PermissionKeys.Add("USR_CAN_EDIT_CONTENT_CATEGORY", false);
            
            foreach(var entry in PermissionKeys)
            {
                if(Model.InstallationType == InstallTypeEnum.full || !nPermissions.permissionKeyExists(entry.Key))
                {
                    nPermissions.DeletePermissionKey(entry.Key);
                    nPermissions.RegisterPermissionKey(entry.Key, entry.Value, "LANG_PERMISSIONS_" + entry.Key);
                }
            }


            //Add Groups
            if(Model.InstallationType == InstallTypeEnum.full)
            {

                BaseRepository<Ren.CMS.Persistence.Domain.PermissionGroup> Groups = new BaseRepository<Ren.CMS.Persistence.Domain.PermissionGroup>();
                
                //Admin Group
                Ren.CMS.Persistence.Domain.PermissionGroup Admin = new Ren.CMS.Persistence.Domain.PermissionGroup();
                Admin.GroupName = "ADMINISTRATORS";
                Admin.IsDefaultGroup = false;
                Admin.IsGuestGroup = false;
                int adminGroupId = (int)Groups.AddAndGetId(Admin);

                //Registered Users Group
                Ren.CMS.Persistence.Domain.PermissionGroup RegisteredUsers = new Ren.CMS.Persistence.Domain.PermissionGroup();
                RegisteredUsers.GroupName = "REGISTERED_USERS";
                RegisteredUsers.IsDefaultGroup = true;
                RegisteredUsers.IsGuestGroup = false;
                int registedUsersGroupId = (int)Groups.AddAndGetId(RegisteredUsers);

                //Registered Users Group
                Ren.CMS.Persistence.Domain.PermissionGroup Guests = new Ren.CMS.Persistence.Domain.PermissionGroup();
                Guests.GroupName = "GUESTS";
                Guests.IsDefaultGroup = false;
                Guests.IsGuestGroup = true;
                int guestsGroupId = (int)Groups.AddAndGetId(Guests);


                //Assign Permissions

                 
                BaseRepository<Ren.CMS.Persistence.Domain.Permissions2Group> Groups2Keys = new BaseRepository<Ren.CMS.Persistence.Domain.Permissions2Group>();
               
                foreach(var entry in PermissionKeys)
                {
                    Ren.CMS.Persistence.Domain.Permissions2Group EntityAdmin = new Ren.CMS.Persistence.Domain.Permissions2Group();
                    EntityAdmin.GroupID = Admin.GroupName;
                    EntityAdmin.Pk = entry.Key;
                    EntityAdmin.Val = true;
                    Groups2Keys.Add(EntityAdmin);

                    Ren.CMS.Persistence.Domain.Permissions2Group EntityUser = new Ren.CMS.Persistence.Domain.Permissions2Group();
                    EntityUser.GroupID = RegisteredUsers.GroupName;
                    EntityUser.Pk = entry.Key;
                    EntityUser.Val = false;
                    Groups2Keys.Add(EntityUser);

                    Ren.CMS.Persistence.Domain.Permissions2Group EntityGuests = new Ren.CMS.Persistence.Domain.Permissions2Group();
                    EntityGuests.GroupID = Guests.GroupName;
                    EntityGuests.Pk = entry.Key;
                    EntityGuests.Val = false;
                    Groups2Keys.Add(EntityGuests);


                }






            }


            return Json(new { success = true });



        }


        public JsonResult InstallFilemanagement(InstallWizardModel Model)
        {
            //Add Filemanagement Cross Browser If needed
            if(Model.InstallationType == InstallTypeEnum.full)
            {
                Ren.CMS.Persistence.Repositories.FilemanagementCrossBrowsersRepository Repo = new Ren.CMS.Persistence.Repositories.FilemanagementCrossBrowsersRepository();
                Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers Chrome = new Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers();
                Chrome.browserFullName = "Google Chrome";
                Chrome.browserID = "chrome";
                Chrome.FileFormat = "m4v";
                Chrome.FileType = "video";

                Repo.Add(Chrome);

                Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers Firefox = new Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers();
                Firefox.browserFullName = "Mozilla Firefox";
                Firefox.browserID = "firefox";
                Firefox.FileFormat = "webm";
                Firefox.FileType = "video";

                Repo.Add(Firefox);


                Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers Default = new Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers();
                Default.browserFullName = "Default Detection";
                Default.browserID = "default";
                Default.FileFormat = "mp4";
                Default.FileType = "video";

                Repo.Add(Default);




                
            }

            return Json(new {success = true});

        }


        public JsonResult InstallContent(InstallWizardModel Model)
        {
           //Attachment Types
            List<nContentAttachmenType> Types = new List<nContentAttachmenType>();
            Types.Add(new nContentAttachmenType() { Handler = new Ren.CMS.Content.ContentAttachmentHandlers.ContentAttachmentHandlerBase(), Name = "DefaultType", StoragePath = "Content" });

            foreach(var t in Types)
            {
                if(nContentAttachmenTypeManager.GetTypeByName(t.Name) == null)
                {
                    nContentAttachmenTypeManager.RegisterAttachmentType(t);
                }

            }

            //Attachment Roles
            nAttachmentRole GALLERY = new nAttachmentRole();
            GALLERY.Rolename = "GALLERY";
            GALLERY.Rolelangline = "LANG_CONTENT_ATTACHMENTROLES_GALLERY";
            GALLERY.Rolelangpackage = "CONTENT";
            GALLERY.Arguments = new List<nAttachmentArgument>();
            GALLERY.Arguments.Add(new nAttachmentArgument() { Argumentlangline = "LANG_CONTENT_ATTACHMENTARGUMENT_INDEXIMG", Argumentlangpackage = "CONTENT", ArgumentName = "INDEXIMG" });
            GALLERY.Arguments.Add(new nAttachmentArgument() { Argumentlangline = "LANG_CONTENT_ATTACHMENTARGUMENT_GALLERYVIEW", Argumentlangpackage = "CONTENT", ArgumentName = "GALLERYVIEW" });
            GALLERY.Arguments.Add(new nAttachmentArgument() { Argumentlangline = "LANG_CONTENT_ATTACHMENTARGUMENT_VIDEO", Argumentlangpackage = "CONTENT", ArgumentName = "VIDEO" });
            GALLERY.Arguments.Add(new nAttachmentArgument() { Argumentlangline = "LANG_CONTENT_ATTACHMENTARGUMENT_FILE", Argumentlangpackage = "CONTENT", ArgumentName = "FILE" });

            if (nAttachmentRoleManager.GetRoleByName("GALLERY") == null)
                nAttachmentRoleManager.RegisterNewRole(GALLERY);



            return Json(new { success = true });
             
        }

        [HttpPost]
        public JsonResult InstallWidgets(InstallWizardModel Model)
        {
           
            string folder = Server.MapPath("~/Install/BackendWidgets/");
            BaseRepository<Ren.CMS.Persistence.Domain.BackendWidget> repo = new BaseRepository<Ren.CMS.Persistence.Domain.BackendWidget>();
            foreach (string f in System.IO.Directory.GetFiles(folder, "*.wdgt.xml"))
            {
                string fname = Path.GetFileName(f);
                string path = Server.MapPath("~/Install/BackendWidgets/" + fname);
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(Ren.CMS.CORE.Backend.BackendWidget));
                FileStream FS = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                using (Stream reader = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    Ren.CMS.CORE.Backend.BackendWidget W = (Ren.CMS.CORE.Backend.BackendWidget)s.Deserialize(reader);
                    repo.Add(new Ren.CMS.Persistence.Domain.BackendWidget() { WidgetName = W.widgetName, WidgetPartialView = W.widgetPartialView, DefinedHeight = W.definedHeight, DefinedWidth = W.definedWidth, Icon = W.Icon, NeededPermission = W.neededPermission });
                }
              
            }


            return Json(new { success = true });

        }

        #endregion 
    }
}