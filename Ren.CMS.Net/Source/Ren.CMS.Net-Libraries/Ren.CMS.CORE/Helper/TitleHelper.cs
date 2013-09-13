namespace Ren.CMS.CORE.Helper.TitleHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class TitleHelper
    {
        #region Fields

        string titleTemplate = "{GS:GLOBALSETTING.GLOBABL_MAINSITENAME} | {INP:INPUT}";

        #endregion Fields

        #region Constructors

        public TitleHelper()
        {
            //Getting Current titleTemplate

            SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

            string currentTPL = GS.Read("GLOBAL_TITLE_TEMPLATE");
            if (!GS.empty(currentTPL))
            {

                titleTemplate = currentTPL;

            }
        }

        #endregion Constructors

        #region Methods

        public string CombineTitle(string TITLE)
        {
            return this.parseTemplate(this.titleTemplate, TITLE);
        }

        private string parseTemplate(string tmpl, string input)
        {
            string pattern = @"{(.+?)\}";
            System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tmpl);

            foreach (Match AP in API)
            {

                string str = AP.Value;
                string col = Regex.Replace(str, pattern, "$1");
                if (col.StartsWith("{GS:"))
                {

                    col = col.Replace("{", "");
                    col = col.Replace("}", "");

                }
                if (col.StartsWith("GS:"))
                {

                    col = col.Replace("GS:", "");

                    SettingsHelper.GlobalSettingsHelper GS = new SettingsHelper.GlobalSettingsHelper();

                    string final = tmpl.Replace(str, GS.Read(col));

                    tmpl = final;

                }

                if (col.StartsWith("{INP:"))
                {

                    col = col.Replace("{", "");
                    col = col.Replace("}", "");

                }
                if (col.StartsWith("INP:"))
                {

                    col = col.Replace("INP:", "");

                    string final = tmpl.Replace(str, input);

                    tmpl = final;

                }

            }

            return tmpl;
        }

        #endregion Methods
    }
}