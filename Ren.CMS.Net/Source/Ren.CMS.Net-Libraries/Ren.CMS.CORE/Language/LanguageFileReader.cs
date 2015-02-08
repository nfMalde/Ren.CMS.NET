using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace Ren.CMS.CORE.Language
{
    public class LanguageFileReader
    {

        private LanguageFile LangFile = null;

        public LanguageFileReader(string filePath)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load(filePath);

            XmlNode Header = Doc.SelectSingleNode("//header");
            string headerTitle = "";
            string headerLangCode = "";
            string headerLangName = "";
            if(Header != null)
            {
                XmlNode Title = Header.SelectSingleNode("//title");
                if (Title != null)
                    headerTitle = Title.InnerText;
                XmlNode LangCode = Header.SelectSingleNode("//langCode");
                if (LangCode != null)
                    headerLangCode = LangCode.InnerText;
                XmlNode LangName = Header.SelectSingleNode("//langName");
                if (LangName != null)
                    headerLangName = LangName.InnerText;
            }
            LanguageFile LFile = new LanguageFile();
            LFile.Header = new LanguageFileHeader() { LangCode = headerLangCode, Title = headerTitle, LangName = headerLangName};
            LFile.Lines = new List<LanguageFileLine>();
            XmlNodeList Packages = Doc.SelectNodes("/languageFile/package");
            foreach(XmlNode Node in Packages)
            {
                foreach (XmlNode line in Node.SelectNodes("line"))
                {

                    XmlNode Name = line.SelectSingleNode("name");

                    XmlNode Value = line.SelectSingleNode("value");

                    LFile.Lines.Add(new LanguageFileLine() { LanguageLineName = Name.InnerText, LanguageLineValue = Value.InnerText, LanguagePackage = Node.Attributes["name"].Value });
                }
            }

            this.LangFile = LFile;
            
        }

        public LanguageFileHeader GetHeader()
        {
            return this.LangFile.Header;
        }

        public List<LanguageFileLine> GetLines()
        {
            return this.LangFile.Lines;

        }
    }

    public class LanguageFileLine
    {
        public string LanguagePackage { get; set; }
        public string LanguageLineName { get; set; }
        public string LanguageLineValue { get; set; }
    }

    public class LanguageFile
    {
        public LanguageFileHeader Header { get; set; }

        public List<LanguageFileLine> Lines { get; set; }
    }


    public class LanguageFileHeader
    {
        public string Title { get; set; }
        public string LangCode { get; set; }
        public string LangName { get; set; }

    }
}
