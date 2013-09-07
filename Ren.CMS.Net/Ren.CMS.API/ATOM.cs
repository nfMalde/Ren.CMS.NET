using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.API.ATOM
{
    using System.Net;
    using System.Xml;
    public class RequestBody
    {
        private List<System.Xml.XmlNode> entries = new List<System.Xml.XmlNode>();
        private XmlDocument Buffer = new XmlDocument();
        public void AddElement(string Name, string innerText, List<System.Xml.XmlAttribute> Attributes = null)
        {
            XmlNode Node = this.Buffer.CreateElement("atom:" + Name);
            Node.InnerText = innerText;

            if (Attributes != null)
            {
                foreach (XmlAttribute Attr in Attributes)
                {
                    Node.Attributes.Append(Attr);

                }

            }

            this.entries.Add(Node);

        }


        public XmlDocument GetXML()
        {
            XmlDocument Document = new XmlDocument();

            XmlNode Entry = Document.CreateElement("atom:entry");
            Entry.Attributes.Append(Buffer.CreateAttribute("xmlns:atom", "http://www.w3.org/2005/Atom"));

            foreach (XmlNode Node in this.entries)
            {

                Entry.AppendChild(Node);


            }

            return Document;
        }

    }

    public class ATOMrequest
    {
        private HttpWebRequest request = null;


        public ATOMrequest(NetworkCredential Credential, string URL)
        {
            this.request = (HttpWebRequest)WebRequest.Create(URL);

            if (Credential != null)
                this.request.Credentials = Credential;



        }

        public HttpWebResponse Execute(string Method, RequestBody Body)
        {

            XmlDocument XmlBody = Body.GetXML();

            this.request.ContentType = "";


            return null;
        }



    }


}
