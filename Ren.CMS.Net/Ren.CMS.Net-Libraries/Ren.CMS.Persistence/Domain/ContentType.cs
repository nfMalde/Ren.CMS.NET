namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentType
    {
        #region Properties

        public virtual string Actionpath
        {
            get; set;
        }

        public virtual string Controller
        {
            get; set;
        }

        public virtual string CreatePartial
        {
            get; set;
        }

        public virtual string EditPartial
        {
            get; set;
        }

        public virtual string Name
        {
            get; set;
        }

        #endregion Properties
    }
}