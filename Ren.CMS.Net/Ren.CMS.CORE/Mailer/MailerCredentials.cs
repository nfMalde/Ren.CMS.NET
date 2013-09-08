namespace Ren.CMS.CORE.Mailer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class MailBodyParser
    {
        #region Fields

        private string body = "";
        private Dictionary<string, string> parsing = new Dictionary<string, string>();

        #endregion Fields

        #region Constructors

        public MailBodyParser(string Body)
        {
            body = Body;
        }

        #endregion Constructors

        #region Properties

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

        #endregion Properties

        #region Methods

        public void Add(string variablename, string variablevalue)
        {
            parsing.Add(variablename, variablevalue);
        }

        public void AddRange(Dictionary<string, string> range)
        {
            foreach (KeyValuePair<string, string> pair in range)
                parsing.Add(pair.Key, pair.Value);
        }

        #endregion Methods
    }

    public class MailerCredentials
    {
        #region Fields

        public bool CredentialRetrievalEnabled = false;
        public string CredentialRetrievalLogin = null;
        public string CredentialRetrievalPassword = null;
        public string CredentialRetrievalPort = null;
        public bool CredentialRetrievalRequiresHTTPS = false;
        public string CredentialRetrievalServer = null;
        public string CredentialRetrievingmode = null;
        public string CredentialSenderMailAddress = null;
        public string CredentialSenderName = null;
        public bool CredentialSMPTRequiresHTTPS = false;
        public string CredentialSMTPLogin = null;
        public string CredentialSMTPPassword = null;
        public string CredentialSMTPPort = null;
        public bool CredentialSMTPRequiredAuthentification = false;
        public string CredentialSMTPServer = null;
        public string CrendtialDefaultMailLayout = "~/MailLayouts/Default/_Mail.cshtml";

        #endregion Fields

        #region Constructors

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

        #endregion Constructors
    }
}