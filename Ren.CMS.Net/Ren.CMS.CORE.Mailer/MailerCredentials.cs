using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.Mailer
{
    public class MailerCredentials
    {
        public MailerCredentials(string SMTPserver, string SMTPPort, string SMTPlogin, string SMTPpassword
                                , string SenderEmailAddress, string SenderName
                                , bool SMTPRequiredAuthentification, bool SMTPRequiresHTTPS
                                , string DefaultMailLayout, bool RetrievalEnabled
                                , string Retrievingmode = "IMAP", string RetrievalServer = ""
                                , string RetrievalPort = "", string RetrievalLogin = ""
                                , string RetrievalPassword = "", bool RetrievalRequiresHTTPS = false)
        {

            CredentialRetrievalEnabled = RetrievalEnabled;
            CredentialRetrievalLogin = RetrievalLogin;
            CredentialRetrievalPassword = RetrievalPassword;
            CredentialRetrievalPort = RetrievalPort;
            CredentialRetrievalRequiresHTTPS = RetrievalRequiresHTTPS;
            CredentialRetrievalServer = RetrievalServer;
            CredentialRetrievingmode = Retrievingmode;

            CredentialSenderMailAddress = SenderEmailAddress;
            CredentialSenderName = SenderName;


            CredentialSMPTRequiresHTTPS = SMTPRequiresHTTPS;
            CredentialSMTPLogin = SMTPlogin;
            CredentialSMTPPassword = SMTPpassword;
            CredentialSMTPPort = SMTPPort;
            CredentialSMTPRequiredAuthentification = SMTPRequiredAuthentification;
            CredentialSMTPServer = SMTPserver;


        }
        public string CredentialSMTPServer = null;
        public string CredentialSMTPPort = null;
        public string CredentialSMTPLogin = null;
        public string CredentialSMTPPassword = null;
        public string CredentialSenderMailAddress = null;
        public string CredentialSenderName = null;
        public bool CredentialSMPTRequiresHTTPS = false;
        public bool CredentialSMTPRequiredAuthentification = false;
        public string CrendtialDefaultMailLayout = "~/MailLayouts/Default/_Mail.cshtml";
        public bool CredentialRetrievalEnabled = false;
        public string CredentialRetrievingmode = null;
        public string CredentialRetrievalLogin = null;
        public string CredentialRetrievalPassword = null;
        public bool CredentialRetrievalRequiresHTTPS = false;
        public string CredentialRetrievalServer = null;
        public string CredentialRetrievalPort = null;



    }


    public class MailBodyParser
    {
        private string body = "";
        public MailBodyParser(string Body)
        {
            body = Body;
        }
        private Dictionary<string, string> parsing = new Dictionary<string, string>();
        public void Add(string variablename, string variablevalue)
        {

            parsing.Add(variablename, variablevalue);


        }

        public void AddRange(Dictionary<string, string> range)
        {
            foreach (KeyValuePair<string, string> pair in range)
                parsing.Add(pair.Key, pair.Value);

        }

        public string ParsedBody
        {
            get
            {
                string input = body;
                string pattern = @"{(.+?)\}";
                System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(input);

                foreach (Match AP in API)
                {


                    string str = AP.Value;
                    string col = Regex.Replace(str, pattern, "$1");
                    if (this.parsing.Where(p => p.Key == col).Count() > 0)
                    {
                        string final = input.Replace(str, this.parsing.Where(p => p.Key == col).First().Value);

                        input = final;
                    }
                }
                return input;


            }


        }
    }
}
