namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TSettingValue
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual int SettingID
        {
            get; set;
        }

        public virtual string SettingValue
        {
            get; set;
        }

        #endregion Properties
    }
}