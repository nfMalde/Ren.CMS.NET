namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Rating
    {
        #region Properties

        public virtual int GroupID
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string LangCode
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