namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SettingModel
    {
        #region Properties

        public virtual System.Nullable<int> Cid
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string SettingDefaultValue
        {
            get; set;
        }

        public virtual string SettingLangLineDescr
        {
            get; set;
        }

        public virtual string SettingLangLineLabel
        {
            get; set;
        }

        public virtual string SettingName
        {
            get; set;
        }

        public virtual string SettingRelation
        {
            get; set;
        }

        public virtual string SettingType
        {
            get; set;
        }

        public virtual string ValueType
        {
            get; set;
        }

        #endregion Properties
    }
}