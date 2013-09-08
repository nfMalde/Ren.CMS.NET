namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Settings2Permission
    {
        #region Properties

        public virtual string BackEndPM
        {
            get; set;
        }

        public virtual string FrontEndPM
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual System.Nullable<int> Sid
        {
            get; set;
        }

        #endregion Properties
    }
}