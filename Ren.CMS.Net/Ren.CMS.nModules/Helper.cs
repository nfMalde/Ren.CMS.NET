using Ren.CMS.CORE.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Ren.CMS.nModules.Helper
{

    class LocaleDisplayName : DisplayNameAttribute
    {
        private string langline;
        private string package;
        private string langcode;
        public LocaleDisplayName(string slangline, string spackage, string slangcode)
            : base()
        {
            this.langline = slangline;
            this.package = spackage;
            if (slangcode == "__USER__")
            {
                slangcode = new UserSettings(new MemberShip.nProvider.CurrentUser().nUser).getSetting("SELECTED_LANGUAGE").Value.ToString();


            }
            this.langcode = slangcode;

        }

        public override string DisplayName
        {
            get
            {
                return new DynamicAttribute(this.langline, this.package, this.langcode).Role;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicAttribute : Attribute
    {

        public string Role
        { get; private set; }

        public DynamicAttribute()
        {



        }
        public DynamicAttribute(string langLine, string package = "root", string langCode = "deDE")
        {
            if (langCode == null) langCode = "deDE";
            Ren.CMS.CORE.Language.Language LGN = new Ren.CMS.CORE.Language.Language(langCode);
            LGN.Package = (package != null ? package : "root");

            string line = LGN.getLine(langLine);
            if (String.IsNullOrEmpty(line)) line = "ERROR: <strong>" + langCode + "/" + package + "/(STRING)" + langLine + "</strong> not found.";
            this.Role = line;
        }


    }




}
