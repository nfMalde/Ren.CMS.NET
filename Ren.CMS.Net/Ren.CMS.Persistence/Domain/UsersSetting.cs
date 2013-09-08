namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UsersSetting
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string SettingName
        {
            get; set;
        }

        public virtual string SettingValue
        {
            get; set;
        }

        public virtual string Upkid
        {
            get; set;
        }

        #endregion Properties
    }
}