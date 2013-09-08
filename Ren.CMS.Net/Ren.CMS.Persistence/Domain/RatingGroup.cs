namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RatingGroup
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string InternalName
        {
            get; set;
        }

        #endregion Properties
    }
}