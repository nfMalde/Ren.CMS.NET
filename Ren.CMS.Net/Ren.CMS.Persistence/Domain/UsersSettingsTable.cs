namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UsersSettingsTable
    {
        #region Properties

        public virtual string DataType
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string SettingDefaultVal
        {
            get; set;
        }

        public virtual string SettingLangLine
        {
            get; set;
        }

        public virtual string SettingLongDescription
        {
            get; set;
        }

        public virtual string SettingName
        {
            get; set;
        }

        public virtual System.Nullable<int> SettingOrder
        {
            get; set;
        }

        public virtual string SType
        {
            get; set;
        }

        #endregion Properties
    }
}