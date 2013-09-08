namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ContentClickCounter
    {
        #region Properties

        public virtual int Cid
        {
            get; set;
        }

        public virtual int Id
        {
            get; set;
        }

        public virtual string Ip
        {
            get; set;
        }

        #endregion Properties
    }
}