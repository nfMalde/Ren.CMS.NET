namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BackendDesktopIcon
    {
        #region Properties

        public virtual string Action
        {
            get; set;
        }

        public virtual string Icon
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string LangLine
        {
            get; set;
        }

        #endregion Properties
    }
}