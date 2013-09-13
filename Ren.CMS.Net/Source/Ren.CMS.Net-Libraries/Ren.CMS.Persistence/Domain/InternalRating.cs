namespace Ren.CMS.Persistence.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InternalRating
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual int Refid
        {
            get; set;
        }

        public virtual int Stars
        {
            get; set;
        }

        public virtual string Topic
        {
            get; set;
        }

        #endregion Properties
    }
}