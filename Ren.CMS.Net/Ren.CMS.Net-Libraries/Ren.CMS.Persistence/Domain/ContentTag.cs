namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentTag
    {
        #region Properties

        public virtual string ContentType
        {
            get; set;
        }

        public virtual int EnableBrowsing
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string TagName
        {
            get; set;
        }

        public virtual string TagNameSEO
        {
            get; set;
        }

        #endregion Properties
    }
}