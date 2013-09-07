using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Ren.CMS.CORE.Language.LanguageDefaults
{
    [System.AttributeUsage(System.AttributeTargets.All)]
    public class LanguagePackage : System.Attribute
    { 
        private string name;
        private MemberInfo[] info;
        public LanguagePackage(string Code)
        {
            this.info = this.GetType().GetMembers();
            if (this.info != null)
            {
                name = Code;
            }
        }
        public string Name
        {
        get{ return this.name; }
        
        }
    
    }


    [System.AttributeUsage(System.AttributeTargets.All)]
    public class LanguageCode: System.Attribute
    { 
        private string name;
        public LanguageCode(string Code)
        {
            name = Code;
            
        }
        public string Name
        {
        get{ return this.name; }
        
        }
    }


    public class LanguageDefaultValues : IEnumerable
    {
        private Dictionary<string, string> myCol = new Dictionary<string, string>();
        private LanguagePackage[] packages = null;
        private LanguageCode[] codes = null;
        private object[] sobjData = null;
        private Type baseType = null;
        private PropertyInfo[] props = null;
        public string Package = "Root";
        public string LangCode = "__USER__";
        private string Langlinename = "";
        public LanguageDefaultValues(string languageLinename, string Packagename = "Root", string LangCode = "__USER__")
        {

            this.Package = Packagename;
            this.LangCode = LangCode;
            this.Langlinename = languageLinename;
           
        }
        public void Add(string langCode, string Value)
        {

           myCol.Add(langCode, Value);
        }
        public IEnumerator<KeyValuePair<string,string>> GetEnumerator()
        {
            return myCol.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private string _getLangName(Expression<Func<string, string>> f)
        {   
           string name =  ((f.Body as MemberExpression).Member.Name);
           return name;
        }
        public string ReturnLangLine()
        {
            Dictionary<string, string> DefaultVal = myCol;
            string LangLine = this.Langlinename;

            string package = this.Package;
            string code = this.LangCode;
           

            CORE.Language.Language Lang = new CORE.Language.Language(code, package);
            
                string langName = this.Langlinename;

                return Lang.getLine(langName, DefaultVal);

            
        
            return "";

        }

        public Dictionary<string, string> ToDictionary() 
        {
            return this.myCol;
        
        
        }
    
    }

    
}