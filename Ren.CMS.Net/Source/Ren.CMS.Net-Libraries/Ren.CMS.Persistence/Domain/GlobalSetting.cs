namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GlobalSetting
    {
        #region Properties

        public virtual string ContentType
        {
            get; set;
        }

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

        public virtual string SType
        {
            get; set;
        }

        #endregion Properties
    }
}