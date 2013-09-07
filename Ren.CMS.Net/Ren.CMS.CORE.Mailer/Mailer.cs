using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ren.CMS.CORE.Mailer
{
    public static class MailType
    {
        public static class Html
        {

            public override string ToString()
            {
                return "Mailtype.Html";
            }

        }
        public static class PlainText
        {

            public override string ToString()
            {
                return "Mailtype.Text";
            }

        }

    }
   
    public class Mailer
    {
        private MailerCredentials MapFromGlobalSettings()
        {
            mapinit_();

            Settings.GlobalSettings GS = new Settings.GlobalSettings();


            return new MailerCredentials(GS.getSetting("MAILER_SMTPSERVER").Value.ToString(),
                                         GS.getSetting("MAILER_SMTP_PORT").Value.ToString(),
                                         GS.getSetting("MAILER_SMTPLOGIN").Value.ToString(),
                                         GS.getSetting("MAILER_SMTP_PASSWORD").Value.ToString(),
                                         GS.getSetting("MAILER_SENDER_EMAILADDRESS").Value.ToString(),
                                         GS.getSetting("MAILER_SENDERNAME").Value.ToString(),
                                         GS.getSetting("MAILER_SMTP_REQUIRES_AUTH").ValueAsBoolean(),
                                         GS.getSetting("MAILER_SMTP_REQUIRES_HTTPS").ValueAsBoolean(),
                                         GS.getSetting("MAILER_DEFAULT_LAYOUT").Value.ToString(),
                                         GS.getSetting("MAILER_RETRIEVAL_ENABLED").toBoolean(),
                                         GS.getSetting("MAILER_RETRIEVAL_MODE").Value.ToString(),

                                         GS.getSetting("MAILER_RETRIEVAL_SERVER").Value.ToString(),
                                         GS.getSetting("MAILER_RETRIEVAL_PORT").Value.ToString(),
                                         GS.getSetting("MAILER_RETRIEVAL_LOGIN").Value.ToString(),
                                         GS.getSetting("MAILER_RETRIEVAL_PASSWORD").Value.ToString(),
                                         GS.getSetting("MAILER_RETRIEVAL_HTTPS").ValueAsBoolean());
        }


        private MailerCredentials Credentials = null;

        public Mailer()
        {
            //TODO: Collect SMTP Data from Global Settings

            //Needed Settings:
            //SMTP Server
            //SMTP Login (Username, Password)
            //POP3 Server + Login
            //IMAP Server + Login
            //Default Sender Mail
            //Default Mail Layout

            this.Credentials = this.MapFromGlobalSettings();

            init_();

        }

        public Mailer(MailerCredentials CredentialData)
        {

            this.Credentials = CredentialData;
            init_();


        }

        private SmtpClient _client = new SmtpClient();
        private void mapinit_()
        {

            Settings.GlobalSettings GS = new Settings.GlobalSettings();
            Dictionary<string, string> SettingsDefaults = new Dictionary<string, string>();
            SettingsDefaults.Add("MAILER_SMTPSERVER", "mail.example.com");
            SettingsDefaults.Add("MAILER_SMTP_PORT", "83");

            SettingsDefaults.Add("MAILER_SMTPLOGIN", "login@example.com");
            SettingsDefaults.Add("MAILER_SMTP_PASSWORD", "pass");
            SettingsDefaults.Add("MAILER_SENDER_EMAILADDRESS", "noreply@example.com");
            SettingsDefaults.Add("MAILER_SENDERNAME", "noreply@example.com");
            SettingsDefaults.Add("MAILER_SMTP_REQUIRES_AUTH", "true");
            SettingsDefaults.Add("MAILER_SMTP_REQUIRES_HTTPS", "false");
            SettingsDefaults.Add("MAILER_DEFAULT_LAYOUT", "~/Maillayouts/default/default.cshtml");
            SettingsDefaults.Add("MAILER_SMTP_REQUIRES_HTTPS", "false");
            SettingsDefaults.Add("MAILER_RETRIEVAL_ENABLED", "");
            SettingsDefaults.Add("MAILER_RETRIEVAL_SERVER", "");
            SettingsDefaults.Add("MAILER_RETRIEVAL_MODE", "IMAP");
            SettingsDefaults.Add("MAILER_RETRIEVAL_PORT", "");
            SettingsDefaults.Add("MAILER_RETRIEVAL_LOGIN", "");
            SettingsDefaults.Add("MAILER_RETRIEVAL_PASSWORD", "");
            SettingsDefaults.Add("MAILER_RETRIEVAL_HTTPS", "false");

            Dictionary<string, string> Labels = new Dictionary<string, string>();
            Labels.Add("MAILER_SMTPSERVER-de-DE", "SMTP Server");
            Labels.Add("MAILER_SMTPSERVER-en-US", "SMTP Server");
            Labels.Add("MAILER_SMTP_PORT-de-DE", "SMTP Port");
            Labels.Add("MAILER_SMTP_PORT-en-US", "SMTP port");
            Labels.Add("MAILER_SMTPLOGIN-de-DE", "Benutzername");
            Labels.Add("MAILER_SMTPLOGIN-en-US", "Login");
            Labels.Add("MAILER_SMTP_PASSWORD-de-DE", "Passwort");
            Labels.Add("MAILER_SMTP_PASSWORD-en-US", "Password");
            Labels.Add("MAILER_SENDER_EMAILADDRESS-de-DE", "Absender Adresse");
            Labels.Add("MAILER_SENDER_EMAILADDRESS-en-US", "Sender Address");
            Labels.Add("MAILER_SENDERNAME-de-DE", "Absender Name");
            Labels.Add("MAILER_SENDERNAME-en-US", "Sender name");
            Labels.Add("MAILER_SMTP_REQUIRES_AUTH-de-DE", "Server erfordert Authentifizierung");
            Labels.Add("MAILER_SMTP_REQUIRES_AUTH-en-US", "Server requires authentification");
            Labels.Add("MAILER_SMTP_REQUIRES_HTTPS-de-DE", "Aktiviere HTTPS");
            Labels.Add("MAILER_SMTP_REQUIRES_HTTPS-en-US", "Aktiviere HTTPS");
            Labels.Add("MAILER_DEFAULT_LAYOUT-de-DE", "Standart-Layout");
            Labels.Add("MAILER_DEFAULT_LAYOUT-en-US", "Default Layout");
            Labels.Add("MAILER_RETRIEVAL_ENABLED-de-DE", "Empfang aktiviert");
            Labels.Add("MAILER_RETRIEVAL_ENABLED-en-US", "Activate Retrieval");

            Labels.Add("MAILER_RETRIEVAL_SERVER-de-DE", "Server");
            Labels.Add("MAILER_RETRIEVAL_SERVER-en-US", "Server");

            Labels.Add("MAILER_RETRIEVAL_MODE-de-DE", "Empfangmodus");
            Labels.Add("MAILER_RETRIEVAL_MODE-en-US", "Retrieval mode");


            Labels.Add("MAILER_RETRIEVAL_PORT-de-DE", "Port");
            Labels.Add("MAILER_RETRIEVAL_PORT-en-US", "Port");


            Labels.Add("MAILER_RETRIEVAL_LOGIN-de-DE", "Benutzername");
            Labels.Add("MAILER_RETRIEVAL_LOGIN-en-US", "Username");
            Labels.Add("MAILER_RETRIEVAL_PASSWORD-de-DE", "Passwort");
            Labels.Add("MAILER_RETRIEVAL_PASSWORD-en-US", "Password");

            Labels.Add("MAILER_RETRIEVAL_HTTPS-de-DE", "Empfangserver erfordert HTTPS");
            Labels.Add("MAILER_RETRIEVAL_HTTPS-en-US", "Retireval server requires HTTPS");


            List<Settings.nSetting> L = GS.listSettings(true);

            foreach (KeyValuePair<string, string> Pair in SettingsDefaults)
            {
                if (L.Where(s => s.Name == Pair.Key).Count() == 0)
                {
                    Settings.nSetting NewOne = new Settings.nSetting();
                    string belongs2o = "";
                    if (Pair.Key.StartsWith("MAILER_SMTP") || Pair.Key.StartsWith("MAILER_SENDERNAME"))
                        belongs2o = "_SMTP";
                    else if (Pair.Key.StartsWith("MAILER_RETRIEVAL"))
                        belongs2o = "_RETRIEVAL";
                    else if (Pair.Key.StartsWith("MAILER_DEFAULT_LAYOUT"))
                        belongs2o = "_LAYOUT_DEFAULT";



                    NewOne.Name = Pair.Key;
                    NewOne.Value = Pair.Value;
                    NewOne.PermissionBackend = "CAN_CHANGE_MAILSETTINGS";
                    NewOne.PermissionFrontend = "CAN_CHANGE_MAILSETTINGS";
                    NewOne.LabelLanguageLine = "LANG_" + Pair.Key;
                    NewOne.SettingRelation = "MAIL_SETTINGS" + belongs2o;
                    NewOne.SettingType = new Settings.nSettingType().SettingString;
                    Language.Language Lang = new Language.Language("__USER__", NewOne.SettingRelation);

                    Dictionary<string, string> LanguageDefaultsx = new Dictionary<string, string>();

                    if (Labels.Where(i => i.Key.StartsWith(Pair.Key) && i.Key.EndsWith("de-DE")).Count() >= 1)
                    {
                        KeyValuePair<string, string> LabelDE = Labels.Where(i => i.Key.StartsWith(Pair.Key) && i.Key.EndsWith("de-DE")).First();

                        LanguageDefaultsx.Add(LabelDE.Key.Replace(Pair.Key + "-", ""), LabelDE.Value);
                    }

                    if (Labels.Where(i => i.Key.StartsWith(Pair.Key) && i.Key.EndsWith("en-US")).Count() >= 1)
                    {
                        KeyValuePair<string, string> LabelENG = Labels.Where(i => i.Key.StartsWith(Pair.Key) && i.Key.EndsWith("en-US")).First();

                        LanguageDefaultsx.Add(LabelENG.Key.Replace(Pair.Key + "-", ""), LabelENG.Value);
                    }
                    string label = Lang.getLine(NewOne.LabelLanguageLine, LanguageDefaultsx);
                    GS.AddSetting(NewOne);

                }

            }


        }
        private void init_()
        {
            int PORT = 0;
            int.TryParse(this.Credentials.CredentialSMTPPort, out PORT);
            SmtpClient MailSender = new SmtpClient(this.Credentials.CredentialSMTPServer,
                                                    PORT);

            MailSender.EnableSsl = this.Credentials.CredentialSMPTRequiresHTTPS;
            if (!String.IsNullOrEmpty(this.Credentials.CredentialSMTPLogin) &&
                    this.Credentials.CredentialSMTPRequiredAuthentification)
                MailSender.Credentials = new NetworkCredential(this.Credentials.CredentialSMTPLogin, this.Credentials.CredentialSMTPPassword);
            _client = MailSender;

        }

        public List<string> ErrorLog = new List<string>();
        public bool SendMail(string To, string Subject, string MailFormat, string Body, ControllerContext controllerContext)
        {
            return SendMail(To, Subject, MailFormat, Body, controllerContext.ParentActionViewContext);

        }
        public bool SendMail(string To, string Subject, string MailFormat, string Body, ViewContext viewContext)
        {
            try
            {
                //TODO: Send Mail
                Models.Core.SendMailModel MailModel = new Models.Core.SendMailModel();
                MailModel.Body = Body;
                MailModel.MailID = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day
                                    + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;


                MailModel.Receiptient = To;
                MailModel.Subject = Subject;

                string HTMLBody = this.RenderPartialViewToString(viewContext, Credentials.CrendtialDefaultMailLayout, MailModel);
                if (MailFormat == "Html")
                {
                    MailMessage MSG = new MailMessage(new MailAddress(Credentials.CredentialSenderMailAddress, Credentials.CredentialSenderName), new MailAddress(To));
                    MSG.Subject = Subject;
                    MSG.Body = HTMLBody;
                    MSG.BodyEncoding = Encoding.ASCII;


                    MSG.IsBodyHtml = true;

                    _client.Send(MSG);
                }
                else
                {
                    string sauce = Body; // htm = your html box
                    HTMLBody = null;
                    Regex myRegex = new Regex(@"(?<=^|>)[^><]+?(?=<|$)", RegexOptions.Compiled);
                    foreach (Match iMatch in myRegex.Matches(sauce))
                    {
                        HTMLBody += Environment.NewLine + iMatch.Value; //txt = your destination box
                    }
                    MailMessage MSG = new MailMessage(new MailAddress(Credentials.CredentialSenderMailAddress, Credentials.CredentialSenderName), new MailAddress(To));
                    MSG.Subject = Subject;
                    MSG.Body = HTMLBody;
                    MSG.BodyEncoding = Encoding.ASCII;


                    MSG.IsBodyHtml = false;

                    _client.Send(MSG);

                }
                return true;
            }
            catch (Exception e)
            {

                this.ErrorLog.Add(e.Message);

            }
            return false;
        }


        private string RenderPartialViewToString(ViewContext pvt, string viewName, object model)
        {
            ViewDataDictionary ViewData = pvt.ViewData;
            TempDataDictionary TempData = pvt.TempData;
            ControllerContext CT = pvt.Controller.ControllerContext;


            if (string.IsNullOrEmpty(viewName))
                viewName = CT.RouteData.GetRequiredString("action");

            ViewData.Model = model;
            Ren.CMS.ViewEngine.nTheming Engin = new Ren.CMS.ViewEngine.nTheming();

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = Engin.FindPartialView(CT, viewName, false);
                ViewContext viewContext = new ViewContext(CT, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }



    }


}
