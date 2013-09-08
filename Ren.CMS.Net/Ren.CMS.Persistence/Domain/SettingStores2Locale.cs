namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SettingStores2Locale
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string LangLine
        {
            get; set;
        }

        public virtual int Stid
        {
            get; set;
        }

        #endregion Properties
    }
}