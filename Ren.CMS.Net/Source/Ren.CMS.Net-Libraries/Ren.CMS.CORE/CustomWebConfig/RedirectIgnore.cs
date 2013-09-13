namespace Ren.CMS.CORE.CustomWebConfig
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    public class RedirectIgnore
    {
        #region Fields

        private List<string> _paths = new List<string>();

        #endregion Fields

        #region Properties

        public IEnumerable<string> IgnoredPaths
        {
            get
            {
            return _paths;

            }
        }

        #endregion Properties

        #region Methods

        public bool CheckUrlIsIgnored(string urlAbsolutePath)
        {
            bool ig = false;
            foreach (string path in _paths)
            {
            string p = path;

            bool ignored = false;

            if (path.StartsWith("~/"))
            {
                p = p.Substring(1);

            }
            bool fullpath = p.EndsWith("/*");

            if (fullpath)
            {

                p = p.Remove(p.LastIndexOf("/*"));

            }

            if (urlAbsolutePath.Trim().ToLower().StartsWith(p.Trim().ToLower()))
              ignored = true;
            else
                ignored = false;

            if (!fullpath)
            {

                if (!urlAbsolutePath.Trim().ToLower().EndsWith(p.Trim().ToLower()))
                {
                    ignored = false;

                }
            }

            if (ignored)
            {
                ig = true;
                break;
            }

            }

            return ig;
        }

        internal void LoadValuesFromXml(XmlNode section)
        {
            foreach (XmlNode node in section.SelectNodes("//path"))
            _paths.Add(node.InnerText);
        }

        #endregion Methods
    }
}