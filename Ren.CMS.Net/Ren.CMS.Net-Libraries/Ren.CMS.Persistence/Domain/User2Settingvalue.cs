namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class User2Settingvalue
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

        public virtual string Uid
        {
            get; set;
        }

        public virtual int Vid
        {
            get; set;
        }

        #endregion Properties
    }
}