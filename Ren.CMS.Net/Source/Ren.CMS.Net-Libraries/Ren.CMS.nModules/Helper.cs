namespace Ren.CMS.nModules.Helper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.Settings;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicAttribute : Attribute
    {
        #region Constructors

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

        #endregion Constructors

        #region Properties

        public string Role
        {
            get; private set;
        }

        #endregion Properties
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LocaleDisplayName : DisplayNameAttribute
    {
        #region Fields

        private string langcode;
        private string langline;
        private string package;

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Properties

        public override string DisplayName
        {
            get
            {
                return new DynamicAttribute(this.langline, this.package, this.langcode).Role;
            }
        }

        #endregion Properties
    }

    public class RequiredIfAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
    {
        private String PropertyName { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == DesiredValue.ToString())
            {
                ValidationResult result = base.IsValid(value, context);
                return result;
            }
            return ValidationResult.Success;
        }
    }
}