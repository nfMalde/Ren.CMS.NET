using Ren.CMS.CORE.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ren.CMS.CORE.Mailer
{ 
    public class Mailer
    {
        private MailerCredentials MapFromGlobalSettings()
        {
            

            Settings.GlobalSettings GS = new Settings.GlobalSettings();


            return new MailerCredentials(GS.getSetting("MAILER_SMTPSERVER").Value.ToString(),
                                         GS.getSetting("MAILER_SMTP_PORT").Value.ToString(),
                                         GS.getSetting("MAILER_SMTPLOGIN").Value.ToString(),
                                         GS.getSetting("MAILER_SMTP_PASSWORD").Value.ToString(),
                                         GS.getSetting("MAILER_SENDER_EMAILADDRESS").Value.ToString(),
                                         GS.getSetting("MAILER_SENDERNAME").Value.ToString(),
                                         GS.getSetting("MAILER_SMTP_REQUIRES_AUTH").ValueAsBoolean(),
                                         GS.getSetting("MAILER_SMTP_REQUIRES_HTTPS").ValueAsBoolean(),
                                         GS.getSetting("MAILER_DEFAULT_LAYOUT").Value.ToString(),false);
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
