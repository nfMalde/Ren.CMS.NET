using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.CORE;
using Ren.CMS.CORE.Settings;
using Ren.CMS.CORE.Language;
using Ren.CMS.CORE.ThisApplication;
using Ren.CMS.CORE.Permissions;
using Ren.CMS.CORE.Language.LanguageDefaults;
using System.IO;

namespace InstallModule.Installer.Controllers
{
    public class InstallerController : Controller
    {
 

        //
        // GET: /Installer/

        public void Index(string id)
        {
            this.RedirectToAction("Install_" + id);

        }

        public JsonResult Install_DBase_Shema(string id = "ALL")
        {


            return Json(new { });
        }

        

        private nSetting SettingReady(string Rel, string PermissionFrontEnd, string PermissionBackEnd, string SettingName, string Value,  string SettingType, string ValueType, string LangLine, List<nSettingStoreItem> Store = null) {

            nSetting Setting = new nSetting();
            Setting.Name = SettingName;
            Setting.PermissionBackend = PermissionBackEnd;
            Setting.PermissionFrontend = PermissionFrontEnd;
            Setting.SettingType = SettingType;
            Setting.ValueType = ValueType;
            Setting.DefaultValue = Value;
            Setting.SettingRelation = Rel;
            Setting.LabelLanguageLine = LangLine;

            if (SettingType == nSettingType.SettingArray)
            {
                if(Store != null)
                {
                    if (Store.Count > 0)
                    {
                        foreach (nSettingStoreItem I in Store)
                            Setting.Store.Add(I);

                    }

                }    
                 
            }

            return Setting;
        }

        private void InsertLangLine(string PCKG, string Name, Dictionary<string,string> LangVals)
        {
            Language Lang = new Language("__USER__", PCKG);
            Lang.getLine(Name, LangVals);
          
            
        }

        public ActionResult Install_MailerSettings()
        {

            GlobalSettings GS = new GlobalSettings();
            bool CAN_MANAGE_MAIL_LAYOUTS = nPermissions.RegisterPermissionKey("CAN_MANAGE_MAIL_LAYOUTS", false, new LanguageDefaultValues("", "")
                                                                                { 
                                                                                    {"de-DE", "Benutzer darf Mail Layouts verwalten."},
                                                                                    {"en-US", "User can manage mail layouts"}
                                                                                });
            bool CAN_MANAGE_SMTP = nPermissions.RegisterPermissionKey("CAN_MANAGE_SMTP", false, new LanguageDefaultValues("", "")
                                                                                { 
                                                                                    {"de-DE", "Benutzer darf Mail Layouts verwalten."},
                                                                                    {"en-US", "User can manage mail layouts"}
                                                                                });


            if (!CAN_MANAGE_MAIL_LAYOUTS)
                Response.Write("CAN_MANAGE_MAIL_LAYOUTS => Unable to register Permissionkey<br/>");
            if (!CAN_MANAGE_SMTP)
                Response.Write("CAN_MANAGE_SMTP => Unable to register Permissionkey<br/>");
            

                                          
            //SMTP Settings:
            GS.AddSetting(this.SettingReady("LAYOUT_SETTINGS_MAILER", "CAN_MANAGE_MAIL_LAYOUTS", "CAN_MANAGE_MAIL_LAYOUTS", "MAILER_DEFAULT_LAYOUT"
                         , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_DEFAULT_LAYOUT"));

            this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTPSERVER", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "SMTP Server"},
                                                                                {"en-US", "SMTP server"}});

           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER","CAN_MANAGE_SMTP","CAN_MANAGE_SMTP", "MAILER_SMTPSERVER"
                         , "mail.example.com", nSettingType.SettingString, nValueType.ValueString,"LANG_MAILER_SMTPSERVER"));

           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTPSERVER", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "SMTP Server"},
                                                                                {"en-US", "SMTP server"}});

           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SMTP_PORT"
                         , "95", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SMTP_PORT"));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTP_PORT", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "SMTP Port"},
                                                                                {"en-US", "SMTP port"}});



           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SMTPLOGIN"
                        , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SMTPLOGIN"));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTPLOGIN", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "Benutzername"},
                                                                                {"en-US", "Login"}});



           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SMTP_PASSWORD"
                        , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SMTP_PASSWORD"));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTP_PASSWORD", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "Passwort"},
                                                                                {"en-US", "Password"}});

           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SENDER_EMAILADDRESS"
                     , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SENDER_EMAILADDRESS"));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SENDER_EMAILADDRESS", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "Absender Emailadresse"},
                                                                                {"en-US", "Sender eMail Address"}});


           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SENDERNAME"
                    , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SENDERNAME"));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SENDERNAME", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "Abesendername"},
                                                                                {"en-US", "Sender name"}});

           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SMTP_REQUIRES_AUTH"
                    , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SMTP_REQUIRES_AUTH", new List<nSettingStoreItem>() { new nSettingStoreItem() { ID = 0, Label = "LANG_SHARED_YES", Value = "true" }, 
                                                                                                                    new nSettingStoreItem() { ID = 0, Label = "LANG_SHARED_NO", Value = "false" } }));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTP_REQUIRES_AUTH", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "Ausgangsserver erfordert Authorisierung"},
                                                                                {"en-US", "SMPT Server requires Credentials"}});
           GS.AddSetting(this.SettingReady("SMTP_SETTINGS_MAILER", "CAN_MANAGE_SMTP", "CAN_MANAGE_SMTP", "MAILER_SMTP_REQUIRES_HTTPS"
              , "", nSettingType.SettingString, nValueType.ValueString, "LANG_MAILER_SMTP_REQUIRES_HTTPS", new List<nSettingStoreItem>() { new nSettingStoreItem() { ID = 0, Label = "LANG_SHARED_YES", Value = "true" }, 
                                                                                                                    new nSettingStoreItem() { ID = 0, Label = "LANG_SHARED_NO", Value = "false" } }));
           this.InsertLangLine("SMTP_SETTINGS_MAILER", "LANG_MAILER_SMTP_REQUIRES_HTTPS", new Dictionary<string, string>(){ 
                                                                                {"de-DE", "Ausgangsserver erfordert HTTPS"},
                                                                                {"en-US", "SMPT Server requires HTTPS"}});




           return Content("Complete") ;
        }


    }
}
