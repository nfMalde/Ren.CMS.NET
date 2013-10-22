namespace Ren.CMS.NET_InstallLauncher
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class GitFile
    {
        #region Properties

        public string encoding
        {
            get; set;
        }

        public string git_url
        {
            get; set;
        }

        public string html_url
        {
            get; set;
        }

        public string name
        {
            get; set;
        }

        public string path
        {
            get; set;
        }

        public string sha
        {
            get; set;
        }

        public long size
        {
            get; set;
        }

        public string type
        {
            get; set;
        }

        public string url
        {
            get; set;
        }

        public Dictionary<string, string> _links
        {
            get; set;
        }

        #endregion Properties
    }

    public partial class launcher : System.Web.UI.Page
    {
        #region Fields

        private string launcherProxy = "http://rencms-proxy.networkfreaks.de/Service";
        private string licenseURL = "https://api.github.com/repos/nfMalde/Ren.CMS.NET/contents/LICENSE";
        private string repoURL = "https://api.github.com/repos/nfMalde/Ren.CMS.NET/contents/Ren.CMS.Net/";

        #endregion Fields

        #region Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var Form = Request.Form;

            if (Form.AllKeys.Any(f => f == "Install"))
            {
                var Install = Form["Install"];
                switch (Install)
                {
                    case "CAB":
                    //Step1: Download CAB
                        JavaScriptSerializer JS = new JavaScriptSerializer();

                        string cabResponseCSS = this.GitGet(repoURL + "Source/Installer-CAB/css");
                        List<GitFile> FilesCSS = JS.Deserialize<List<GitFile>>(cabResponseCSS);

                        string cabResponseJS = this.GitGet(repoURL + "Source/Installer-CAB/js");
                        List<GitFile> FilesJS = JS.Deserialize<List<GitFile>>(cabResponseJS);

                        foreach (GitFile CSS in FilesCSS)
                        {

                            Extract(CSS, ("~/Launcher/css"), "Source/Installer-CAB/css");

                        }

                        foreach (GitFile JSf in FilesJS)
                        {

                            Extract(JSf, ("~/Launcher/js"), "Source/Installer-CAB/js");

                        }

                        string LicenseResponse = GitGet(licenseURL);

                        GitFile LicenseFile = JS.Deserialize<GitFile>(LicenseResponse);

                        Extract(LicenseFile, ("~/Launcher"), "");

                    //Step3: Download License File

                    //Step4: Ready
                    break;

                    case "DOWNLOAD":
                        //Step 5: Download Current Build

                        //Step 6: Extract and Test

                        //Step 7: Redirect

                    break;

                }
            }
        }

        private void Extract(GitFile f, string destination, string RepoPath)
        {
            string newP =  destination;
            if(newP.StartsWith("~/"))
            {
                newP = newP.Replace("~/", "");

            }

            string[] dirPaths = newP.Split('/');

            string PathSoFar = "~";

            foreach (string path in dirPaths)
            {
                PathSoFar += "/" + path;

                if (!Directory.Exists(Server.MapPath(PathSoFar)))
                    Directory.CreateDirectory(Server.MapPath(PathSoFar));

            }

            if (f.type == "dir")
            {
                string dirDestinationx = destination + (destination.EndsWith("/") ? f.name : "/" + f.name);
                JavaScriptSerializer JS = new JavaScriptSerializer();

                string _repo = repoURL + RepoPath +"/"+ f.name;

                string dir_contents = GitGet(_repo);

                List<GitFile> gitFile = new List<GitFile>();
                gitFile = JS.Deserialize<List<GitFile>>(dir_contents);

                gitFile.ForEach(g => Extract(g, dirDestinationx, RepoPath + "/" + f.name));

            }

            if (f.type == "file")
            {
                WebClient WCX = new WebClient();

                WCX.DownloadFile(f.url, Server.MapPath(destination + "/" + f.name));

            }
        }

        private string GitGet(string url)
        {
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        #endregion Methods
    }
}