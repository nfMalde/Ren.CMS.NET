namespace Ren.CMS.CORE.Language.LanguageDefaults
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web;

    [System.AttributeUsage(System.AttributeTargets.All)]
    public class LanguageCode : System.Attribute
    {
        #region Fields

        private string name;

        #endregion Fields

        #region Constructors

        public LanguageCode(string Code)
        {
            name = Code;
        }

        #endregion Constructors

        #region Properties

        public string Name
        {
            get{ return this.name; }
        }

        #endregion Properties
    }

    public class LanguageDefaultValues : IEnumerable
    {
        #region Fields

        public string LangCode = "__USER__";
        public string Package = "Root";

        private Type baseType = null;
        private LanguageCode[] codes = null;
        private string Langlinename = "";
        private Dictionary<string, string> myCol = new Dictionary<string, string>();
        private LanguagePackage[] packages = null;
        private PropertyInfo[] props = null;
        private object[] sobjData = null;

        #endregion Fields

        #region Constructors

        public LanguageDefaultValues(string languageLinename, string Packagename = "Root", string LangCode = "__USER__")
        {
            this.Package = Packagename;
            this.LangCode = LangCode;
            this.Langlinename = languageLinename;
        }

        #endregion Constructors

        #region Methods

        public void Add(string langCode, string Value)
        {
            myCol.Add(langCode, Value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return myCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        private string _getLangName(Expression<Func<string, string>> f)
        {
            string name =  ((f.Body as MemberExpression).Member.Name);
               return name;
        }

        #endregion Methods
    }

    [System.AttributeUsage(System.AttributeTargets.All)]
    public class LanguagePackage : System.Attribute
    {
        #region Fields

        private MemberInfo[] info;
        private string name;

        #endregion Fields

        #region Constructors

        public LanguagePackage(string Code)
        {
            this.info = this.GetType().GetMembers();
            if (this.info != null)
            {
                name = Code;
            }
        }

        #endregion Constructors

        #region Properties

        public string Name
        {
            get{ return this.name; }
        }

        #endregion Properties
    }
}