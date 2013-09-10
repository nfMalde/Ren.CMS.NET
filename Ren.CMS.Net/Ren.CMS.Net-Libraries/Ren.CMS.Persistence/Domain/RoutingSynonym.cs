namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RoutingSynonym
    {
        #region Properties

        public virtual string Action
        {
            get; set;
        }

        public virtual string Controller
        {
            get; set;
        }

        public virtual string Name
        {
            get; set;
        }

        public virtual string Rpath
        {
            get; set;
        }

        #endregion Properties
    }
}