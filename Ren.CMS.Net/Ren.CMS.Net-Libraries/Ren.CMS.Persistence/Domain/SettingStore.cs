namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SettingStore
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual int Sid
        {
            get; set;
        }

        public virtual string Val
        {
            get; set;
        }

        #endregion Properties
    }
}