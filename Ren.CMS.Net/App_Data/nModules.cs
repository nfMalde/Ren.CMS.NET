using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using nfCMS_NET.CORE.Settings;
namespace nfCMS_NET.nModules.Helper {

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
    public class DynamicAttribute : Attribute {

        public string Role
        { get; private set; }

        public DynamicAttribute() { 
        
        
        
        }
        public DynamicAttribute(string langLine, string package="root", string langCode="deDE") {
            if(langCode == null) langCode = "deDE";
            nfCMS_NET.CORE.Language.Language LGN = new nfCMS_NET.CORE.Language.Language(langCode);
            LGN.Package = (package != null ? package : "root");

            string line = LGN.getLine(langLine);
            if (String.IsNullOrEmpty(line)) line = "ERROR: <strong>"+ langCode +"/" + package + "/(STRING)" + langLine + "</strong> not found.";
            this.Role = line;
        }
    
    
    }




}
namespace nfCMS_NET.nModules
{
    public class nModules
    { 
        private string module = "undefined";

        public nModules() { 
        
        }
        public nModules(string iModuleName) {

            this.ModuleName = iModuleName;
        }
        public string ModuleName {
            get {

                return this.module;
            
            }
            set {

                this.module =(!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value)? value : "undefined");
            
            }
        
        
        }
        public string  ViewPath(string controller,string view) {
       
            if (this.module == "undefined") throw new Exception("Modulename not set. Please register your module in the controllers");
            string myPath = HttpContext.Current.Server.MapPath("~/_Modules/" + this.module + "/Views/" + controller + "/" + view);
            if (!System.IO.File.Exists(myPath)) throw new Exception("View: \"" + myPath + "\" not found.");


            return "~/_Modules/" + this.module + "/Views/" + controller + "/" + view;

         
        
        }

      

    }
}