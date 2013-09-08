namespace Ren.CMS.CORE.nhibernate.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InternalProContra
    {
        #region Properties

        public virtual int Id
        {
            get; set;
        }

        public virtual string PText
        {
            get; set;
        }

        public virtual string PType
        {
            get; set;
        }

        public virtual int Refid
        {
            get; set;
        }

        #endregion Properties
    }
}